// Conversión de números a letras para notas y cheques (México / Moneda Nacional)
export function numeroALetras(monto: number): string {
  if (isNaN(monto) || monto === null || monto === undefined) return 'CERO PESOS 00/100 M.N.';

  const partes = Math.abs(monto).toFixed(2).split('.');
  const entero = parseInt(partes[0], 10);
  const centavos = partes[1] || '00';

  if (entero === 0) {
    return `CERO PESOS ${centavos}/100 M.N.`;
  }

  const letras = convertirGrupo(entero);
  const moneda = entero === 1 ? 'PESO' : 'PESOS';
  return `${letras} ${moneda} ${centavos}/100 M.N.`.trim();
}

function convertirGrupo(n: number): string {
  if (n === 0) return '';
  if (n === 100) return 'CIEN';

  const unidades = ['', 'UN', 'DOS', 'TRES', 'CUATRO', 'CINCO', 'SEIS', 'SIETE', 'OCHO', 'NUEVE'];
  const especiales = ['DIEZ', 'ONCE', 'DOCE', 'TRECE', 'CATORCE', 'QUINCE', 'DIECISEIS', 'DIECISIETE', 'DIECIOCHO', 'DIECINUEVE'];
  const decenas = ['', '', 'VEINTE', 'TREINTA', 'CUARENTA', 'CINCUENTA', 'SESENTA', 'SETENTA', 'OCHENTA', 'NOVENTA'];
  const centenas = ['', 'CIENTO', 'DOSCIENTOS', 'TRESCIENTOS', 'CUATROCIENTOS', 'QUINIENTOS', 'SEISCIENTOS', 'SETECIENTOS', 'OCHOCIENTOS', 'NOVECIENTOS'];

  if (n < 10) return unidades[n];
  if (n < 20) return especiales[n - 10];
  if (n < 30) {
    if (n === 20) return 'VEINTE';
    return 'VEINTI' + unidades[n - 20];
  }
  if (n < 100) {
    const d = Math.floor(n / 10);
    const u = n % 10;
    return decenas[d] + (u > 0 ? ' Y ' + unidades[u] : '');
  }
  if (n < 1000) {
    const c = Math.floor(n / 100);
    const r = n % 100;
    return centenas[c] + (r > 0 ? ' ' + convertirGrupo(r) : '');
  }
  if (n < 1000000) {
    const miles = Math.floor(n / 1000);
    const r = n % 1000;
    const prefijoMiles = miles === 1 ? 'UN MIL' : convertirGrupo(miles) + ' MIL';
    return prefijoMiles + (r > 0 ? ' ' + convertirGrupo(r) : '');
  }
  if (n < 1000000000) {
    const millones = Math.floor(n / 1000000);
    const r = n % 1000000;
    const prefijoMillones = millones === 1 ? 'UN MILLON' : convertirGrupo(millones) + ' MILLONES';
    return prefijoMillones + (r > 0 ? ' ' + convertirGrupo(r) : '');
  }

  return n.toString();
}
