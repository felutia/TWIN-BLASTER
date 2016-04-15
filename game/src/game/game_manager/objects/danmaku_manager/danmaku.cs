/*!
 *  @file       danmaku.cs
 *  @brief      ボス弾幕ベース
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
	public class IDanmaku
		
	{
		public IDanmaku( DIFFICULTY diff, COLOR_ID color )
		{
			// 難易度設定
			m_Difficulty = diff;
			
			// 
			m_Color = color;
			
			m_Active = true;
			
		}
		
		/// <summary>
		///初期化
		/// </summary>
		public virtual void Initialize()
		{
			
		} 
		
		/// <summary>
		/// 更新
		/// </summary>
		public virtual void Update ()
		{
			if(m_Active == false)return;
			
			for( int i = 0; i < m_attack_time_num; ++i )
			{
				if( m_AttackTimer[i] != null )
				{
					// 攻撃タイマー更新
					m_AttackTimer[i].Update( );
				}
			}
		}
		
		/// <summary>
        /// 弾の作成
        /// </summary>
        /// <param name='type'>
        /// Type.
        /// </param>
        /// <param name='pos'>
        /// Position.
        /// </param>
        /// <param name='angle'>
        /// Angle.
        /// </param>
        /// <param name='speed'>
        /// Speed.
        /// </param>
        /// <param name='power'>
        /// Power.
        /// </param>
        public void CreateBullet( ENEMY_BULLET_ID id, COLOR_ID color, bool reflec, Vector2 pos, float direction, float speed ,int p)
        {
			
			
			IBoss boss = (IBoss)CGameManager.GetInstance().FindObject(OBJECT_ID.BOSS);
			
			if( boss != null )
				CEnemyBulletManager.GetInstance( ).Create( id, OBJECT_ID.ENEMY_BULLET, color, reflec, boss.Pos + boss.Sprite.Center + pos, direction, speed, 1 );
		}
		
		
		public void CreateBULL(ENEMY_BULLET_ID id, COLOR_ID color, bool reflec, Vector2 pos, float direction, float speed ,int p)
		{
			IBoss boss = (IBoss)CGameManager.GetInstance().FindObject(OBJECT_ID.BOSS);
			
			if( boss != null )
				CEnemyBulletManager.GetInstance( ).Create( id, OBJECT_ID.ENEMY_BULLET, color, reflec, new Vector2(boss.Pos.X + boss.Sprite.Center.X + pos.X ,pos.Y), direction, speed, 1 );
		}
		
		
		/// <summary>
		/// 解放
		/// </summary>
		public  void Dispose ()
		{
			m_Active = false;
		}
		
		
		
		public bool Active
		{
			get{return m_Active;}
			set{m_Active = value;}
		}
		
		/// <summary>
		/// 難易度
		/// </summary>
		protected DIFFICULTY			m_Difficulty;
		
		/// <summary>
		/// 攻撃間隔
		/// </summary>
		protected uint					m_attack_time;
		
		/// <summary>
		/// 攻撃タイマー数
		/// </summary>
		protected const uint			m_attack_time_num = 10;
		
		/// <summary>
		/// ボス攻撃間隔設定
		/// </summary>
		protected aqua.CFrameTimer[]	m_AttackTimer;
		
		/// <summary>
		/// バレットの数
		/// </summary>
		protected int 					m_BulletNum;
		
        /// <summary>
        /// 射出角度
        /// </summary>
        protected float[]		 		m_Angle;
		
		protected DANMAKU_ID			m_Id;
		
		protected COLOR_ID				m_Color;
		
		protected bool     				m_Active;
		
		
	}
}

