Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports SICDIN30

<TestClass>
Public Class AutoUpdaterTests

    <TestMethod>
    Public Sub ParseVersion_StandardAndTags_ParsesCorrectly()
        Dim v1 As Version = AutoUpdater.ParseVersion("v1.5")
        Assert.AreEqual(1, v1.Major)
        Assert.AreEqual(5, v1.Minor)

        Dim v2 As Version = AutoUpdater.ParseVersion("1.5.2")
        Assert.AreEqual(1, v2.Major)
        Assert.AreEqual(5, v2.Minor)
        Assert.AreEqual(2, v2.Build)

        Dim v3 As Version = AutoUpdater.ParseVersion("v2.0.0.1")
        Assert.AreEqual(2, v3.Major)
        Assert.AreEqual(0, v3.Minor)
        Assert.AreEqual(0, v3.Build)
        Assert.AreEqual(1, v3.Revision)

        Dim vEmpty As Version = AutoUpdater.ParseVersion("")
        Assert.AreEqual(0, vEmpty.Major)
        Assert.AreEqual(0, vEmpty.Minor)
    End Sub

    <TestMethod>
    Public Sub IsNewerVersion_Comparisons_WorkExpectedly()
        Dim current As New Version(1, 5, 0, 0)

        ' Versiones mayores
        Assert.IsTrue(AutoUpdater.IsNewerVersion(current, "v1.5.1"))
        Assert.IsTrue(AutoUpdater.IsNewerVersion(current, "v1.6"))
        Assert.IsTrue(AutoUpdater.IsNewerVersion(current, "v2.0"))

        ' Versiones iguales o menores
        Assert.IsFalse(AutoUpdater.IsNewerVersion(current, "v1.5.0.0"))
        Assert.IsFalse(AutoUpdater.IsNewerVersion(current, "v1.5"))
        Assert.IsFalse(AutoUpdater.IsNewerVersion(current, "v1.0.1"))
        Assert.IsFalse(AutoUpdater.IsNewerVersion(current, "v1.0"))
        Assert.IsFalse(AutoUpdater.IsNewerVersion(current, ""))
    End Sub

    <TestMethod>
    Public Sub CheckForUpdates_CanQueryReleases_ReturnsReleaseInfo()
        ' Prueba de integración de lectura a GitHub API público
        Dim info As ReleaseInfo = AutoUpdater.CheckForUpdates(timeoutSeconds:=10)
        If info IsNot Nothing Then
            Assert.IsFalse(String.IsNullOrEmpty(info.TagName), "TagName no debe ser vacío")
            Assert.IsFalse(String.IsNullOrEmpty(info.DownloadUrl), "DownloadUrl no debe ser vacío")
            Assert.IsTrue(info.AssetName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase), "El asset objetivo debe ser un ZIP")
        End If
    End Sub

End Class

