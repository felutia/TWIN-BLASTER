
/*!
 *  @file       File.cs
 *  @brief      ファイル操作
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/05/09
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.IO;

namespace aqua
{
    /// <summary>
    /// 外部ファイルの読み込みクラス
    /// </summary>
    public sealed class CFile
    {
        /// <summary>
        /// ファイルがあるか調べる
        /// </summary>
        public static bool Exists( string file_name )
        {
            return File.Exists( m_root_path + file_name );
        }
        
        /// <summary>
        /// ファイル読み込み
        /// </summary>
        /// <param name='file_name'>
        /// ファイル名
        /// </param>
        public static byte[] Read( string file_name )
        {
            // 受取り用バッファ
            byte[] buffer = null;
            
            try
            {
                // ファイルを読み取り専用モードで開く
                // usingを使用すると例外が発生した場合でも自動的Disposeされる
                using( FileStream fs = new FileStream( m_root_path + file_name, FileMode.Open, FileAccess.Read ) )
                {
                    try
                    {
                        // 受取りバッファ生成
                        buffer = new byte[fs.Length];
                        
                        // 読み込み
                        fs.Read( buffer, 0, (int)fs.Length );
                    }
                    catch( Exception e )
                    {
                        CDebug.Error( e.Message );
                    }
                    
                    // ストリームを閉じる
                    fs.Close( );
                }
            }
            // ファイルがない
            catch( FileNotFoundException e )
            {
                CDebug.Error( e.Message );
            }
            catch( Exception e )
            {
                CDebug.Error( e.Message );
            }
            
            return buffer;
        }
        
        /// <summary>
        /// 読み込みファイルのルートパス
        /// </summary>
        private const string m_root_path = "/Application/";
    }
}
