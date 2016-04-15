using System;
using Sce.PlayStation.Core;

namespace game
{
	public class CDanmakuManager
	{
		public CDanmakuManager()
		{
		}
		
		// <summary>
        /// インスタンスの取得
        /// </summary>
        /// <returns>
        /// インスタンス
        /// </returns>
        public static CDanmakuManager GetInstance( )
        {
            if( m_Instance == null )
                m_Instance = new CDanmakuManager( );
            
            return m_Instance;
        }
		
		/// <summary>
		/// 初期化
		/// </summary>
		public void Initialize()
		{
		}
		
		/// <summary>
		/// 更新
		/// </summary>
		public void Update()
		{
		}
		
//		/// <summary>
//		/// 生成
//		/// </summary>
//		public void Create( DANMAKU_ID danmaku_id, OBJECT_ID object_id, DIFFICULTY diff, COLOR_ID color )
//		{
//			IDanmaku danmaku = null;
//			
//			switch (danmaku_id)
//			{
//			case DANMAKU_ID.SAMPLE_RED:		danmaku = new CDanmakuSample( diff,color );		break;	
//			case DANMAKU_ID.SAMPLE2_BLUE:	danmaku = new CDanmakuSample2( diff,color );	break;
//			case DANMAKU_ID.SAMPLE3:	danmaku = new CDs3( diff ,color ) ; 				break;
//			case DANMAKU_ID.SAMPLE4:	danmaku = new CDs4( diff ,color ) ; 				break;
//			case DANMAKU_ID.SAMPLE5:	danmaku = new CDs5( diff ,color ) ; 				break;
//			case DANMAKU_ID.SAMPLE6:	danmaku = new CDs6( diff ,color ) ; 				break;
//			case DANMAKU_ID.SAMPLE7:	danmaku = new CDs7( diff ,color ) ; 				break;
//			case DANMAKU_ID.SAMPLE8:	danmaku = new CDs8( diff ,color ) ; 				break;
//			case DANMAKU_ID.SAMPLE9:	danmaku = new CDs9( diff ,color ) ; 				break;
//			}
//			
//			m_Color = color;
//			
//			if(danmaku == null)
//				return;
//			
//			// 初期化処理
//			danmaku.Initialize( );
//			
//			// オブジェクトID設定
//			//danmaku.ObjectID = object_id;
//		}
//		
		public COLOR_ID Color
		{
			get { return m_Color; }
			set { m_Color = value; }
		}
		
		/// <summary>
		/// インスタンス
		/// </summary>
		private static CDanmakuManager  	m_Instance;
		
		private COLOR_ID					m_Color;
	}
}

