/*!
 *  @file       enemy_bullet_manager.cs
 *  @brief      敵弾管理
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
	public class CEnemyBulletManager
	{
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CEnemyBulletManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CEnemyBulletManager( );
            
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
		public void Create( ENEMY_BULLET_ID bullet_id, OBJECT_ID object_id, COLOR_ID color, bool reflec, Vector2 pos, float direction, float speed, int power )
		{
			IEnemyBullet ebullet = null;
			
			switch (bullet_id) 
			{
			case ENEMY_BULLET_ID.NORMAL_BULLET:		ebullet = new CNormalBullet( color, reflec, pos, direction, speed, power );		break;
			case ENEMY_BULLET_ID.MISSILE_BULLET:	ebullet = new CMissileBullet( color, reflec, pos, direction, speed, power );	break;
			//case ENEMY_BULLET_ID.AIM_BULLET:		ebullet = new CAimBullet( color, reflec, pos, direction, speed, power );		break;
			case  ENEMY_BULLET_ID.RAIN_BULLET:		ebullet = new CRainBullet(color , reflec , pos , direction ,speed ,power);		break;
			
			}
			
			if( ebullet == null )
				return;
			
			// 初期化処理
			ebullet.Initialize( );
			
            // オブジェクトタイプ設定
            ebullet.ObjectID = object_id;
		}
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CEnemyBulletManager ()
		{
		}
		
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CEnemyBulletManager  m_Instance;
	}
}