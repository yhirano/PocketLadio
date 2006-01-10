﻿using System;
using System.Runtime.InteropServices;

namespace PocketLadio.Util
{
    /// <summary>
    /// サウンドに関するユーティリティ
    /// </summary>
    public class Sound
    {
        private Sound()
        {
        }

        public static void PlaySound(string strFileName)
        {
            Helpers.PlaySound(strFileName, IntPtr.Zero, Helpers.PlaySoundFlags.SND_FILENAME | Helpers.PlaySoundFlags.SND_ASYNC);
        }

        internal class Helpers
        {
            [Flags]
            public enum PlaySoundFlags : int
            {
                SND_SYNC = 0x0000,		// play synchronously (default)
                SND_ASYNC = 0x0001,		// play asynchronously
                SND_NODEFAULT = 0x0002,	// silence (!default) if sound not found
                SND_MEMORY = 0x0004,	// pszSound points to a memory file
                SND_LOOP = 0x0008,		// loop the sound until next sndPlaySound
                SND_NOSTOP = 0x0010,	// don't stop any currently playing sound
                SND_NOWAIT = 0x00002000,	// don't wait if the driver is busy
                SND_ALIAS = 0x00010000,		// name is a registry alias
                SND_ALIAS_ID = 0x00110000,	// alias is a predefined ID
                SND_FILENAME = 0x00020000,	// name is file name
                SND_RESOURCE = 0x00040004	// name is resource name or atom
            }

            [DllImport("coredll")]
            public static extern bool PlaySound(string szSound, IntPtr hMod, PlaySoundFlags flags);
        }
    }
}
