using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CNormalBossBullet
		: IBossBullet
	{
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public CNormalBossBullet ( COLOR_ID color, bool reflec, Vector2 pos, float direction, float speed, int power )
			: base( color, reflec, pos, direction, speed, power )
		{
		}
		
		/// <summary>
		/// 初期化
		/// </summary>
		public override void Initialize ()
		{
			// スプライト生成
			m_Sprite = CSpriteManager.Create( TEXTURE_ID.E_BULLET);
			
			// 色設定
			if( m_Color == COLOR_ID.BLUE )
				m_Sprite.Color = new Vector4( 0.4f, 0.4f, 1.0f, 1.0f );
			else if( m_Color == COLOR_ID.RAINBOW )
            	m_Sprite.Color = CColorManager.GetColor( );
			else if ( m_Color == COLOR_ID.RED )
				m_Sprite.Color = new Vector4( 1.0f, 0.4f, 0.4f, 1.0f );
			else if( m_Color == COLOR_ID.WHITE )
				m_Sprite.Color = new Vector4( 1.0f, 1.0f, 1.0f, 1.0f );
			
			// アルファブレンドを半加算に設定する
			m_Sprite.Alphablend = aqua.ALPHABLEND_TYPE.TRANSADD;
			
			// サイズ設定
			m_Size = new aqua.CSize( 16.0f, 16.0f );
			
			// 中心を設定
			m_Sprite.Center = new Vector2( 8.0f, 8.0f );
			
			// 回転値を設定
			m_Sprite.Angle = FMath.Radians( m_Direction );
			
			// 位置設定
			m_Sprite.Position = m_Pos;
			
			// 弾の半径
			m_Radius = 8.0f;
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public override void Update ()
		{
			CPlayer2 play2 = (CPlayer2)CGameManager.GetInstance( ).FindObject(OBJECT_ID.PLAYER2);
			
			if( play2 != null )
			{
				if( play2.BombState != CPlayer2.BOMB_STATE.CREATE )
				{
					Vector2 pos = m_Sprite.Position;
					
					pos.X -= FMath.Cos( FMath.Radians( m_Direction ) ) * m_Speed;
					pos.Y -= FMath.Sin( FMath.Radians( m_Direction ) ) * m_Speed;
					
					m_Sprite.Position = pos;
					
					// 画面外判定
					CheckArea( m_ReflecFlag );
				}
			}
			else
			{
				Vector2 pos = m_Sprite.Position;
					
					pos.X -= FMath.Cos( FMath.Radians( m_Direction ) ) * m_Speed;
					pos.Y -= FMath.Sin( FMath.Radians( m_Direction ) ) * m_Speed;
					
					m_Sprite.Position = pos;
					
					// 画面外判定
					CheckArea( m_ReflecFlag );
			}
		}
	}
}

