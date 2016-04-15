/*!
 *  @file       player_bullet.cs
 *  @brief      プレイヤー弾ベースクラス
 *  @author     Riki Ito
 *  @date       2015/03/19
 *  @since      1.0
 *
 *  Copyright (c) 2014 2015, Riki Ito. All rights reserved.
 */

using Sce.PlayStation.Core;

namespace game
{
    /// <summary>
    /// バレットクラス
    /// </summary>
    public abstract class IPlayerBullet
		: IObject
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IPlayerBullet( Vector2 pos, float direction, float speed, int power )
			:base( )
        {
			// 位置
			m_Pos		= pos;
			
            // 向き
            m_Direction = direction;
            
            // 速度
            m_Speed     = speed;
            
            // 威力
            m_Power     = power;
        }
		
		/// <summary>
		/// 解放
		/// </summary>
		public override void Dispose ()
		{
			m_Sprite.Dispose( );
		}
		
        /// <summary>
        /// スプライトの取得
        /// </summary>
        /// <value>
        /// スプライト
        /// </value>
        public aqua.CSprite Sprite
        {
            get { return m_Sprite; }
        }
        
        /// <summary>
        /// サイズの取得
        /// </summary>
        /// <value>
        /// サイズ
        /// </value>
        public aqua.CSize Size
        {
            get { return m_Size; }
        }
        
        /// <summary>
        /// 威力取得
        /// </summary>
        /// <value>
        /// 威力
        /// </value>
        public int Power
        {
            get { return m_Power; }
        }
		
		/// <summary>
		/// 画面端判定
		/// </summary>
		/// <param name='reflec'>
		/// 反射フラグ
		/// </param>
        protected void CheckArea( )
		{
			Vector2 pos = m_Sprite.Position;
			
#if true
			
				if( m_Sprite.Position.X < -8.0f )
	            {
	                m_Sprite.Color.A -= 0.3f;
					
					if( m_Sprite.Color.A <= 0.0f )
						m_IsActive = false;
				}
	            
	            if( m_Sprite.Position.X > 960.0f + 8.0f )
	        	{
					m_Sprite.Color.A -= 0.3f;
					
					if( m_Sprite.Color.A <= 0.0f )
						m_IsActive = false;
				}
	            
	            if( m_Sprite.Position.Y < 128.0f - 8.0f )
				{
					m_Sprite.Color.A -= 0.3f;
					
					if( m_Sprite.Color.A <= 0.0f )
						m_IsActive = false;
				}
			
				if( m_Sprite.Position.Y > 544.0f + 8.0f )
	            {
					m_Sprite.Color.A -= 0.3f;
						
					if( m_Sprite.Color.A <= 0.0f )
						m_IsActive = false;
				}
				
				m_Sprite.Angle = FMath.Radians( m_Direction );
				
#else
				if( pos.X <= 0.0f || pos.X >= 960.0f - 16.0f )
				{
					if( reflec == true )
					{
						m_Speed *= -1.0f;
						
						m_Direction *= -1.0f;
						
						m_Sprite.Angle = FMath.Radians( m_Direction + 180.0f );
						
						m_ReflecFlag = false;
					}
					else if( reflec == false )
					{
	                	m_IsActive = false;
					}
				}
				
				if( pos.Y <= 0.0f || pos.Y >= 544.0f - 16.0f )
				{
					if( reflec == true )
					{
						m_Direction *= -1.0f;
						
						m_Sprite.Angle = FMath.Radians( m_Direction );
						
						m_ReflecFlag = false;
					}
					else if( reflec == false )
					{
						m_IsActive = false;
					}
				}
				
#endif
		}
		
		/// <summary>
		/// エフェクトの作成
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
		public void CreateEffect( EFFECT_ID id, COLOR_ID color, Vector2 pos )
		{
			CEffectManager.GetInstance( ).Create( id, OBJECT_ID.EFFECT, color, pos );
		}
		
        /// <summary>
        /// スプライトクラス
        /// </summary>
        protected aqua.CSprite		m_Sprite;
        
        /// <summary>
        /// 弾のサイズ
        /// </summary>
        protected aqua.CSize    	m_Size;
        
        /// <summary>
        /// 向き
        /// </summary>
        protected float         	m_Direction;
        
        /// <summary>
        /// 弾速
        /// </summary>
        protected float         	m_Speed;
        
        /// <summary>
        /// 威力
        /// </summary>
        protected int           	m_Power;
		
		/// <summary>
        /// 弾の半径
        /// </summary>
        protected float         	m_Radius;
		
		/// <summary>
		/// 位置
		/// </summary>
		protected Vector2			m_Pos;
		
		/// <summary>
		/// 反射フラグ
		/// </summary>
		protected bool				m_ReflecFlag;
    }
}
