/*!
 *  @file       effect_manager.cs
 *  @brief      エフェクトマネージャー
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CEffectManager
	{
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CEffectManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CEffectManager( );
            
            return m_Instance;
        }
		
		/// <summary>
		/// エフェクト生成
		/// </summary>
		/// <param name='bullet_id'>
		/// Bullet_id.
		/// </param>
		/// <param name='object_id'>
		/// Object_id.
		/// </param>
		/// <param name='pos'>
		/// Position.
		/// </param>
		/// <param name='direction'>
		/// Direction.
		/// </param>
		/// <param name='speed'>
		/// Speed.
		/// </param>
		/// <param name='power'>
		/// Power.
		/// </param>
		public void Create( EFFECT_ID effect_id, OBJECT_ID object_id, COLOR_ID color, Vector2 pos )
		{
			m_EffectBase = null;
			
			switch (effect_id) 
			{
			case EFFECT_ID.HIT_EFFECT:			m_EffectBase = new CHitEffect( color, pos );		break;
			case EFFECT_ID.VERNIER_EFFECT:		m_EffectBase = new CVernierParticle( color, pos );	break;
			case EFFECT_ID.BOMB_EFFECT:			m_EffectBase = new CBombEffect( color, pos );		break;
			case EFFECT_ID.REPEL_EFFECT:		m_EffectBase = new CRepelEffect( color, pos );		break;
			case EFFECT_ID.MASUPA_EFFECT:		m_EffectBase = new CMasupa_Effect( color, pos );	break;
			case EFFECT_ID.DEAD_EFFECT:			m_EffectBase = new CDeadEffect( color, pos );		break;
			case EFFECT_ID.RESONANT_EFFECT:		m_EffectBase = new CResonantEffect( color, pos);	break;
			case EFFECT_ID.LASER_EFFECT:		m_EffectBase = new CLaserEffect( color, pos );		break;
			case EFFECT_ID.CHARGE_EFFECT:		m_EffectBase = new CChargeEffect( color, pos );		break;
			}
			
			if( m_EffectBase == null )
				return;
			
			// 初期化処理
			m_EffectBase.Initialize();
			
			// オブジェクトタイプ設定
			m_EffectBase.ObjectID = object_id;
		}
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CEffectManager ()
		{
		}
		
		/// <summary>
		/// エフェクトベースアクセッサ
		/// </summary>
		/// <value>
		/// エフェクトベース
		/// </value>
		public IEffect EffectBase
		{
			get { return m_EffectBase; }
		}
		
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CEffectManager	m_Instance;
		
		/// <summary>
		/// エフェクトベースクラス
		/// </summary>
		private IEffect					m_EffectBase;
	}
}