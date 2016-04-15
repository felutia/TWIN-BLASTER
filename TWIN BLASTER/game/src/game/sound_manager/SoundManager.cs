
/*!
 *  @file       SoundManager.cs
 *  @brief      SE管理
 *  @author     Kazuya Maruyama
 *  @date       2014/09/01
 *  @since      1.0
 *
 *  Copyright (c) 2013, Kazuya Maruyama. All rights reserved.
 */

using Sce.PlayStation.Core;
using System.Collections.Generic;

namespace game
{
    /// <summary>
    /// サウンドID
    /// </summary>
    public enum SOUND_ID
    {
        SELECT,         // 選択音
        DECISION,       // 決定音
        CANCEL,         // キャンセル音
		DAMEGE,         // 被弾音
		BOMB,           // 破壊音
		DEAD,			// 自機死亡音
		DRAIN,          // 吸収音
		LVUP,           // LVUP音
		STAGE_LVUP,     // LVUP音
		RED_BOM,        // 赤ボム
		BLUE_BOM,       // 青ボム
		PAUSE,          // ポーズ
		PAUSE_SELECT,   // ポーズセレクト
		LASER,			// レーザー音
		RESONAT,		// 共鳴音
    }
    
    /// <summary>
    /// SE管理クラス
    /// </summary>
    class CSoundManager
    {
        /// <summary>
        /// インスタンス取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CSoundManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CSoundManager( );
            
            return m_Instance;
        }
        
        /// <summary>
        /// サウンド読み込み
        /// </summary>
        public void Load( )
        {
            foreach( var s in m_sound_name )
                aqua.CSound.Load( s );
        }
        
        /// <summary>
        /// サウンド解放
        /// </summary>
        public void Unload( )
        {
            aqua.CSound.Unload( );
        }
        
        /// <summary>
        /// SE再生
        /// </summary>
        /// <param name='id'>
        /// サウンドID
        /// </param>
        public void Play( SOUND_ID id, float volume )
        {
            aqua.CSound.Play( m_sound_name[(int)id], volume );
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        private CSoundManager( )
        {
        }
        
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CSoundManager    m_Instance;
        
        /// <summary>
        /// サウンドパス
        /// </summary>
        private static readonly string[]  m_sound_name = new string[]
        {
            "data/sound/select.wav",        	// 選択音
            "data/sound/decision.wav",      	// 決定音
            "data/sound/cancel.wav",        	// キャンセル音
			"data/sound/Damege_01.wav",     	// 被弾音
			"data/sound/Bom_30.wav",        	// 破壊音
			"data/sound/Bom_20.wav",        	// 自機死亡音
			"data/sound/Drain.wav",         	// 吸収音
			"data/sound/LVUP.wav",          	// LVUP音
			"data/sound/ta_ta_byuwawan01.wav",	// StageLVUP音
			"data/sound/RedBom.wav",        	// 赤ボム
			"data/sound/BlueBom.wav",       	// 青ボム
			"data/sound/Pause.wav",         	// ポーズ
			"data/sound/Pause_Select.wav",   	// ポーズセレクト
			"data/sound/laser.wav",				// レーザー音
			"data/sound/status03.wav",			// 共鳴音
        };
    }
}
