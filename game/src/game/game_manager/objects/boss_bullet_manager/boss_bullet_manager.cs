/*!
 *  @file       boss_bullet_manager.cs
 *  @brief      ボス弾マネージャー
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
	public class CBossBulletManager
	{
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CBossBulletManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CBossBulletManager( );
            
            return m_Instance;
        }
		
		/// <summary>
		/// 敵弾生成
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
		public void Create( BOSS_BULLET_ID bullet_id, OBJECT_ID object_id, COLOR_ID color, bool reflec, Vector2 pos, float direction, float speed, int power )
		{
			IBossBullet bbullet = null;
			
			switch (bullet_id) 
			{
			case BOSS_BULLET_ID.NORMAL_BULLET:	bbullet = new CMissileBossBullet( color, reflec, pos, direction, speed, power );		break;
			case BOSS_BULLET_ID.MISSILE_BULLET:	bbullet = new CMissileBossBullet( color, reflec, pos, direction, speed, power );	break;
			}
			
			if( bbullet == null )
				return;
			
			// 初期化処理
			bbullet.Initialize( );
			
            // オブジェクトタイプ設定
            bbullet.ObjectID = object_id;
		}
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CBossBulletManager ()
		{
		}
		
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CBossBulletManager  m_Instance;
	}
}