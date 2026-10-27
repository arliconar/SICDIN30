import { useState } from 'react';
import { api } from './api';
import { User as UserIcon, Sparkles, Car, Bike } from 'lucide-react';

export const AVATAR_PRESETS = [
  { id: 'Hombre', label: 'Hombre', icon: UserIcon, color: 'text-blue-400 bg-blue-500/10 border-blue-500/30' },
  { id: 'Mujer', label: 'Mujer', icon: Sparkles, color: 'text-pink-400 bg-pink-500/10 border-pink-500/30' },
  { id: 'Carro', label: 'Carro', icon: Car, color: 'text-amber-400 bg-amber-500/10 border-amber-500/30' },
  { id: 'Bicicleta', label: 'Bicicleta', icon: Bike, color: 'text-emerald-400 bg-emerald-500/10 border-emerald-500/30' },
  { id: 'Flor', label: 'Flor', icon: Sparkles, color: 'text-purple-400 bg-purple-500/10 border-purple-500/30' },
];

interface UserAvatarProps {
  foto?: string;
  nombre?: string;
  className?: string;
  size?: 'xs' | 'sm' | 'md' | 'lg' | 'xl';
}

export default function UserAvatar({ foto, nombre = 'Usuario', className = '', size = 'md' }: UserAvatarProps) {
  const [imgError, setImgError] = useState(false);
  const customUrl = api.resolverAvatarUrl(foto);

  const sizeClasses = {
    xs: 'w-6 h-6 text-[10px]',
    sm: 'w-8 h-8 text-xs',
    md: 'w-10 h-10 text-sm',
    lg: 'w-12 h-12 text-base',
    xl: 'w-16 h-16 text-xl'
  }[size];

  if (customUrl && !imgError) {
    return (
      <img
        src={customUrl}
        alt={nombre}
        className={`${sizeClasses} ${className} rounded-xl object-cover border border-slate-700 bg-slate-800 shadow-sm shrink-0`}
        onError={() => setImgError(true)}
      />
    );
  }

  const preset = AVATAR_PRESETS.find(p => p.id.toLowerCase() === (foto || '').toLowerCase()) || AVATAR_PRESETS[0];
  const IconComp = preset.icon;

  return (
    <div className={`${sizeClasses} ${className} rounded-xl flex items-center justify-center border font-bold shrink-0 ${preset.color} shadow-sm`}>
      <IconComp className="w-1/2 h-1/2" />
    </div>
  );
}
