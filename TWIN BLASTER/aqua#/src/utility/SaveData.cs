
/*!
 *  @file       SaveData.cs
 *  @brief      セーブデータ操作
 *  @author     Kazuya Maruyama
 *  @date       2013-2014/01/03
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace aqua
{
    /// <summary>
    /// セーブデータ操作クラス
    /// 内部的にファイルを生成し、読み書きに対応
    /// </summary>
    public static class CSaveData
    {
        /// <summary>
        /// ファイルがあるか調べる
        /// </summary>
        public static bool Exists( )
        {
            return File.Exists( m_file_name );
        }
        
        /// <summary>
        /// ファイル読み込み
        /// </summary>
        /// <param name='file_name'>
        /// ファイル名
        /// </param>
        public static object Read( )
        {
            // バイナリ変換クラス生成
            BinaryFormatter bf = new BinaryFormatter( );
            
            // 受取り用バッファ
            object buffer = null;
            
            try
            {
                // ファイルを読み取り専用モードで開く
                // usingを使用すると例外が発生した場合でも自動的Disposeされる
                using( FileStream fs = new FileStream( m_file_name, FileMode.Open, FileAccess.Read ) )
                {
                    try
                    {
                        // バイナリデータに変換して取得
                        buffer = bf.Deserialize( fs );
                    }
                    catch( Exception e )
                    {
                        try
                        {
                            byte[] t = new byte[fs.Length];
                            
                            // 読み込みに失敗した場合は直接ストリームから再読み込み
                            fs.Read( t, 0, (int)fs.Length );
                            
                            buffer = t;
                        }
                        catch( Exception e2 )
                        {
                            CDebug.Error( e.Message );
                            CDebug.Error( e2.Message );
                        }
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
        /// ファイル書き出し
        /// </summary>
        /// <param name='file_name'>
        /// ファイル名
        /// </param>
        /// <param name='save_data'>
        /// 書き出すセーブデータ
        /// </param>
        public static void Write( object save_data )
        {
            // バイナリ変換クラス生成
            BinaryFormatter bf = new BinaryFormatter( );
            
            try
            {
                // ファイルを書き出しモードで開く
                using( FileStream fs = new FileStream( m_file_name, FileMode.Create, FileAccess.Write ) )
                {
                    // 書き出し
                    bf.Serialize( fs, save_data );
                    
                    // ストリームを閉じる
                    fs.Close( );
                }
            }
            // 空き容量不足
            catch( IOException e )
            {
                CDebug.Error( e.Message );
                
                // 中途半端に書きだされたファイルがあれば削除
                if( File.Exists( m_file_name ) == true )
                    File.Delete( m_file_name );
            }
            catch( Exception e )
            {
                CDebug.Error( e.Message );
            }
        }
        
        /// <summary>
        /// セーブデータファイル名
        /// </summary>
        private const string m_file_name = "/Documents/save_data.dat";
    }
}
