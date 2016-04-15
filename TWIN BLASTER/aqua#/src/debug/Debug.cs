
/*!
 *  @file       Debug.cs
 *  @brief      デバッグ管理
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;

namespace aqua
{
    /// <summary>
    /// デバッグクラス
    /// </summary>
    public sealed class CDebug
    {
        /// <summary>
        /// アプリケーション出力にメッセージ表示
        /// </summary>
        /// <param name='message'>
        /// メッセージ
        /// </param>
        public static void WriteLine( string message )
        {
#if DEBUG
            Console.WriteLine( message );
#endif
        }
        
        /// <summary>
        /// アプリケーション出力にメッセージ表示
        /// </summary>
        /// <param name='format'>
        /// 表示フォーマット
        /// </param>
        /// <param name='args'>
        /// 表示オブジェクト
        /// </param>
        public static void WriteLine( string format, params object[] args )
        {
#if DEBUG
            for( int i = 0; i < args.Length; ++i )
            {
                if( args[i] != null )
                    format = format.Replace( "{" + i.ToString( ) + "}", args[i].ToString( ) );
                else
                    format = format.Replace( "{" + i.ToString( ) + "}", "" );
            }
            
            Console.WriteLine( format );
#endif
        }
        
        /// <summary>
        /// アプリケーション出力にエラーメッセージ表示
        /// </summary>
        /// <param name='message'>
        /// メッセージ
        /// </param>
        public static void Error( string message )
        {
#if DEBUG
            Console.WriteLine( "[AQUA ERROR]" + message );
#endif
        }
    }
}
