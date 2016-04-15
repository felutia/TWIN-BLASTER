
using Sce.PlayStation.Core;

namespace game
{
    /// <summary>
    /// バレットクラス
    /// </summary>
    public abstract class IBossBullet
		: IObject
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IBossBullet( COLOR_ID color , bool reflec, Vector2 pos, float direction, float speed, int power )
			: base()
        {
			// 色設定
			m_Color				= color;
		
			// 反射フラグ
			m_ReflecFlag		= reflec;
			
            // 位置設定
            m_Pos				= pos;
            
            // 向き
            m_Direction         = direction;
            
            // 速度
            m_Speed             = speed;
            
            // 威力
            m_Power             = power;
        }
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update()
		{
		}
        
		/// <summary>
		/// 解放
		/// </summary>
		public override void Dispose ()
		{
			m_Sprite.Dispose();
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
		
		public Vector2 GetPos
		{
			get { return m_Pos; }
			set { m_Pos = value;}
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
        /// 色取得
        /// </summary>
        /// <value>
        /// 色
        /// </value>
        public COLOR_ID Color
        {
            get { return m_Color; }
        }
		
        /// <summary>
        /// 画面外チェック
        /// </summary>
        protected void CheckArea( bool reflec )
        {
            Vector2 pos = m_Sprite.Position;
            
            if( m_Sprite.Position.X < 0.0f )
            {
				m_ReflecFlag = false;
				Active = false;
            }
            
            if( m_Sprite.Position.X > 960.0f - 16.0f )
        	{
				if( reflec == true )
				{
		            m_Sprite.Position.X = 960.0f - 16.0f;
		            
		            m_Direction = FMath.Degrees( FMath.PI + 2.0f * FMath.Radians( 0.0f ) - FMath.Radians( m_Direction ) );
					
					m_ReflecFlag = false;
				}
				else if( reflec == false )
				{
	                m_Sprite.Color.A -= 0.3f;
					
					if( m_Sprite.Color.A < 0.0f )
						Active = false;
				}
				
			}
            
            if( m_Sprite.Position.Y < 128.0f )
            {
				if( reflec == true )
				{
	                m_Sprite.Position.Y = 128.0f;
	                
	                m_Direction = FMath.Degrees( FMath.PI + 2.0f * FMath.Radians( 90.0f ) - FMath.Radians( m_Direction ) );
					
					m_ReflecFlag = false;
				}
				else if(reflec == false )
				{
	                m_Sprite.Color.A -= 0.3f;
					
					if( m_Sprite.Color.A < 0.0f )
						Active = false;
				}
            }
            
            if( m_Sprite.Position.Y > 544.0f - 16.0f )
            {
				if( reflec == true )
				{
	                m_Sprite.Position.Y = 544.0f - 16.0f;
	                
	                m_Direction = FMath.Degrees( FMath.PI + 2.0f * FMath.Radians( 270.0f ) - FMath.Radians( m_Direction ) );
					
					m_ReflecFlag = false;
	            }
				else if(reflec == false )
				{
	                m_Sprite.Color.A -= 0.3f;
					
					if( m_Sprite.Color.A < 0.0f )
						Active = false;
				}
			}
			
            m_Sprite.Angle = FMath.Radians( m_Direction );
			
			// ヒットエフェクト
                //CreateEffect(EFFECT_ID.HIT_EFFECT, m_Sprite.Position );
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
			CEffectManager.GetInstance( ).Create( id, OBJECT_ID.EFFECT,color, pos );
		}
		
		protected COLOR_ID		m_Color;
		
        /// <summary>
        /// スプライトクラス
        /// </summary>
        protected aqua.CSprite  m_Sprite;
        
        /// <summary>
        /// 弾のサイズ
        /// </summary>
        protected aqua.CSize    m_Size;
        
        /// <summary>
        /// 向き
        /// </summary>
        protected float         m_Direction;
        
        /// <summary>
        /// 弾速
        /// </summary>
        protected float         m_Speed;
        
        /// <summary>
        /// 威力
        /// </summary>
        protected int           m_Power;
		
		/// <summary>
        /// 弾の半径
        /// </summary>
        protected float         m_Radius;
		
		/// <summary>
		/// 位置
		/// </summary>
		protected Vector2		m_Pos;
		
		/// <summary>
		/// 反射フラグ
		/// </summary>
		protected bool			m_ReflecFlag;
    }
}
