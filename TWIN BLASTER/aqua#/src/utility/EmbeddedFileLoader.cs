
/*!
 *  @file       EmbeddedFileLoader.cs
 *  @brief      組み込みファイルローダークラス
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/04/23
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System.IO;
using System.Reflection;

namespace aqua.core
{
    /// <summary>
    /// 組み込みファイルロードクラス
    /// </summary>
    public static class CEmbeddedFileLoader
    {
        /// <summary>
        /// 組み込みファイルの読み込み
        /// </summary>
        /// <param name='file_path'>
        /// ファイルへのパス
        /// </param>
        public static byte[] Load( string file_path )
        {
            // アセンブリ取得
            Assembly assembly = Assembly.GetExecutingAssembly( );

            // リソースの読み込みストリーム取得
            Stream stream = assembly.GetManifestResourceStream( file_path );
            
            // バッファ作成
            byte[] buffer = new byte[stream.Length];
            
            // 読み込み
            stream.Read( buffer, 0, buffer.Length );
            
            return buffer;
        }
    }
}

