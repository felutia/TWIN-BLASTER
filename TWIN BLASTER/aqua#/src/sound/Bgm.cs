
/*!
 *  @file       Bgm.cs
 *  @brief      BGM
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using System;
using Sce.PlayStation.Core.Audio;
using System.Collections.Generic;

namespace aqua
{
    /// <summary>
    /// BGMクラス
    /// ファイル形式はmp3のみ
    /// 多重再生不可
    /// </summary>
    public static class CBgm
    {
        /// <summary>
        /// 読み込み
        /// </summary>
        /// <param name='file_path'>
        /// ファイルパス
        /// </param>
        public static void Load( string file_path )
        {
            // データが読み込まれていたら解放
            if( m_BgmPlayer != null )
                Unload( );
            
            try
            {
                // BGM生成
                m_Bgm = new Bgm( "/Application/" + file_path );
                
                // プレイヤー生成
                m_BgmPlayer = m_Bgm.CreatePlayer( );
            }
            catch( Exception e )
            {
                CDebug.Error( e.Message );
            }
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        public static void Unload( )
        {
            if( m_BgmPlayer != null )
            {
                m_BgmPlayer.Stop( );
                
                m_BgmPlayer.Dispose( );
                
                m_BgmPlayer = null;
            }
            
            if( m_Bgm != null )
            {
                m_Bgm.Dispose( );
                
                m_Bgm = null;
            }
        }
        
        /// <summary>
        /// 再生
        /// </summary>
        public static void Play( )
        {
            Play( true );
        }
        
        /// <summary>
        /// 再生
        /// </summary>
        /// <param name='loop'>
        /// ループ再生
        /// </param>
        public static void Play( bool loop )
        {
            if( m_BgmPlayer == null ) return;
            
            // ループ再生設定
            m_BgmPlayer.Loop = loop;
            
            // BGM再生
            m_BgmPlayer.Play( );
        }
        
        /// <summary>
        /// 再生停止
        /// </summary>
        public static void Stop( )
        {
            if( m_BgmPlayer == null ) return;
            
            m_BgmPlayer.Stop( );
        }
        
        /// <summary>
        /// 一時停止
        /// </summary>
        public static void Pause( )
        {
            if( m_BgmPlayer == null ) return;
            
            m_BgmPlayer.Pause( );
        }
        
        /// <summary>
        /// 一時停止からの再開
        /// </summary>
        public static void Resume( )
        {
            if( m_BgmPlayer == null ) return;
            
            m_BgmPlayer.Resume( );
        }
        
        /// <summary>
        /// ボリュームの取得と設定
        /// 数値範囲は0.0f～1.0f
        /// </summary>
        /// <value>
        /// ボリューム値
        /// </value>
        public static float Volume
        {
            get
            {
                if( m_BgmPlayer == null ) return 0.0f;
                
                return m_BgmPlayer.Volume;
            }
            
            set
            {
                if( m_BgmPlayer == null ) return;
                
                // 最小・最大値チェック
                if( value < 0.0f )      value = 0.0f;
                else if( value > 1.0f ) value = 1.0f;
                
                m_BgmPlayer.Volume = value;
            }
        }
        
        /// <summary>
        /// 曲の長さ取得
        /// </summary>
        /// <value>
        /// 曲の長さ(秒)
        /// </value>
        public static double Duration
        {
            get
            {
                if( m_BgmPlayer == null ) return 0.0;
                
                return m_BgmPlayer.Duration;
            }
        }
        
        /// <summary>
        /// 再生時間の取得と設定
        /// </summary>
        /// <value>
        /// 再生時間
        /// </value>
        public static double Time
        {
            get
            {
                if( m_BgmPlayer == null ) return 0.0;
                
                return m_BgmPlayer.Time;
            }
            
            set
            {
                if( m_BgmPlayer == null ) return;
                
                if( value < 0.0 )
                    value = 0.0;
                else if( value > m_BgmPlayer.Duration )
                    value = m_BgmPlayer.Duration;
                
                m_BgmPlayer.Time = value;
            }
        }
        
        /// <summary>
        /// BGM
        /// </summary>
        private static Bgm          m_Bgm       = null;
        
        /// <summary>
        /// BGM再生クラス
        /// </summary>
        private static BgmPlayer    m_BgmPlayer = null;
    }
}
