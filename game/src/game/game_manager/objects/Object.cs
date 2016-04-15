
/*!
 *  @file       Object.cs
 *  @brief      タスクオブジェクト
 *  @author     Kazuya Maruyama
 *  @date       2014/10/08
 *  @since      1.0
 *
 *  Copyright (c) 2013 2014, Kazuya Maruyama. All rights reserved.
 */

namespace game
{
    /// <summary>
    /// オブジェクトクラス
    /// </summary>
    public abstract class IObject
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IObject( )
        {
            // アクティブフラグON
            m_IsActive = true;
            
            // オブジェクトリストに登録
            CGameManager.GetInstance( ).AddObject( this );
        }
        
        /// <summary>
        /// 初期化
        /// </summary>
        public virtual void Initialize( )
        {
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        public abstract void Update( );
        
        /// <summary>
        /// 解放
        /// </summary>
        public virtual void Dispose( )
        {
            // オブジェクトリストから解放
            CGameManager.GetInstance( ).RemoveObject( this );
        }
        
        /// <summary>
        /// オブジェクトタイプの取得と設定
        /// </summary>
        /// <value>
        /// オブジェクトタイプ
        /// </value>
        public OBJECT_ID ObjectID
        {
            get { return m_ObjectID;  }
            set { m_ObjectID = value; }
        }
        
        /// <summary>
        /// アクティブフラグの取得と設定
        /// </summary>
        /// <value>
        /// アクティブフラグ
        /// </value>
        public bool Active
        {
            get { return m_IsActive;  }
            set { m_IsActive = value; }
        }
        
        /// <summary>
        /// オブジェクトID
        /// </summary>
        protected OBJECT_ID     m_ObjectID;
        
        /// <summary>
        /// アクティブフラグ
        /// </summary>
        protected bool          m_IsActive;
    }
}
