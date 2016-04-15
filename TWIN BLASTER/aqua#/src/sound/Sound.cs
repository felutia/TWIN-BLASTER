
/*!
 *  @file       Sound.cs
 *  @brief      サウンドエフェクト
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
    /// サウンドエフェクトクラス
    /// ファイル形式はwavのみ
    /// 多重再生可
    /// </summary>
    public static class CSound
    {
        /// <summary>
        /// 読み込み
        /// </summary>
        /// <param name='file_path'>
        /// ファイルパス
        /// </param>
        public static void Load( string file_path )
        {
            // テーブル内にあれば読み込まない
            if( m_SoundTable.ContainsKey( file_path ) == true )
                return;
            
            // テーブルにサウンドがない
            try
            {
                // サウンド生成
                Sound data = new Sound( "/Application/" + file_path );
                
                // テーブルに追加
                m_SoundTable.Add( file_path, data );
            }
            catch( Exception e )
            {
                CDebug.Error( e.Message );
            }
        }
        
        /// <summary>
        /// すべて解放
        /// </summary>
        public static void Unload( )
        {
            // サウンド解放
            foreach( var sound in m_SoundTable )
                sound.Value.Dispose( );
            
            // リストクリア
            m_SoundTable.Clear( );
        }
        
        /// <summary>
        /// 解放
        /// </summary>
        /// <param name='file_path'>
        /// ファイルパス
        /// </param>
        public static void Unload( string file_path )
        {
            // テーブルになければ終了
            if( m_SoundTable.ContainsKey( file_path ) == false )
                return;
            
            // サウンド解放
            m_SoundTable[file_path].Dispose( );
            
            // テーブルから外す
            m_SoundTable.Remove( file_path );
        }
        
        /// <summary>
        /// 再生
        /// </summary>
        /// <param name='file_path'>
        /// ファイルパス
        /// </param>
        public static void Play( string file_path )
        {
            Play( file_path, 1.0f );
        }
        
        /// <summary>
        /// 再生
        /// </summary>
        /// <param name='file_path'>
        /// ファイルパス
        /// </param>
        /// <param name='volume'>
        /// ボリューム
        /// </param>
        public static void Play( string file_path, float volume )
        {
            // テーブルになければ終了
            if( m_SoundTable.ContainsKey( file_path ) == false )
                return;
            
            try
            {
                // サウンドプレイヤー生成
                SoundPlayer player = m_SoundTable[file_path].CreatePlayer( );
                
                if( volume < 0.0f )         volume = 0.0f;
                else if( volume > 1.0f )    volume = 1.0f;
                
                // ボリューム設定
                player.Volume = volume;
                
                // サウンドエフェクト再生
                player.Play( );
                
                // 再生中リストに追加
                m_SoundPlayerList.Add( player );
            }
            catch( Exception e )
            {
                CDebug.Error( e.Message );
            }
        }
        
        /// <summary>
        /// 再生終了チェック
        /// </summary>
        internal static void CheckEnd( )
        {
            if( m_SoundPlayerList.Count <= 0 ) return;
            
            for( int i = 0; i < m_SoundPlayerList.Count; )
            {
                // 再生が終了して停止状態のものを見つけたら解放する
                if( m_SoundPlayerList[i].Status == SoundStatus.Stopped )
                {
                    m_SoundPlayerList[i].Dispose( );
                    
                    m_SoundPlayerList.Remove( m_SoundPlayerList[i] );
                    
                    continue;
                }
                
                ++i;
            }
        }
        
        /// <summary>
        /// サウンドテーブル
        /// </summary>
        private static Dictionary<string, Sound>    m_SoundTable        = new Dictionary<string, Sound>( );
        
        /// <summary>
        /// 再生中のプレイヤーリスト
        /// </summary>
        private static List<SoundPlayer>            m_SoundPlayerList   = new List<SoundPlayer>( );
    }
}
