/*!
 *  @file       player_bullet_manager.cs
 *  @brief      プレイヤー弾マネージャー
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
	public class CPlayerBulletManager
	{
        /// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CPlayerBulletManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CPlayerBulletManager( );
            
            return m_Instance;
        }
		
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CPlayerBulletManager( )
		{
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
		public void Create( PLAYER_BULLET_ID bullet_id, OBJECT_ID object_id, Vector2 pos, float direction, float speed, int power )
		{
			IPlayerBullet pbullet = null;
			
			switch (bullet_id)
			{
				case PLAYER_BULLET_ID.INITIA_BULLET: 	pbullet = new CInitiaBullet( pos, direction, speed, power );	break;
//				case PLAYER_BULLET_ID.HOMOING_BULLET:	pbullet = new CHomingBullet( pos, direction, speed, power );	break;
			}
			
			if( pbullet == null )
				return;
			
			// 初期化処理
			pbullet.Initialize( );
			
			pbullet.ObjectID = object_id;
		}
		
        /// <summary>
        /// インスタンス
        /// </summary>
        private static CPlayerBulletManager		m_Instance;
	}
}