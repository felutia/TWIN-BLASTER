
/*!
 *  @file       DrawObject.cs
 *  @brief      描画用オブジェクト
 *  @author     Kazuya Maruyama
 *  @date       2014/01/02
 *  @since      2.7
 *
 *  Copyright (c) 2013-2014, Kazuya Maruyama. All rights reserved.
 */

using Sce.PlayStation.Core;

namespace aqua.core
{
    /// <summary>
    /// 描画オブジェクトベースクラス
    /// </summary>
    public abstract class IDrawObject
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public IDrawObject( )
        {
            // 表示設定ON
            Visible = true;
            
            // 優先度設定
            Priority = 99999;
            
            // アルファブレンド設定
            Alphablend = ALPHABLEND_TYPE.TRANS;
            
            // トランスフォーム行列生成
            m_Transform = Matrix4.Identity;
            
            // 描画オブジェクトID設定
            m_DrawObjectID = DRAW_OBJECT_ID.DUMMY;
        }
        
        /// <summary>
        /// 更新処理
        /// </summary>
        public virtual void Update( )
        {
        }
        
        /// <summary>
        /// 描画処理
        /// </summary>
        public virtual void Draw( )
        {
        }
        
        /// <summary>
        /// 描画オブジェクトID取得
        /// </summary>
        /// <value>
        /// 描画オブジェクトID
        /// </value>
        public DRAW_OBJECT_ID DrawObjectID
        {
            get { return m_DrawObjectID;  }
        }
        
        /// <summary>
        /// 表示設定
        /// true:表示
        /// false:非表示
        /// </summary>
        public bool                 Visible;
        
        /// <summary>
        /// 描画優先度
        /// ゼロが奥、プラスが手前
        /// </summary>
        public uint                 Priority;
        
        /// <summary>
        /// アルファブレンド
        /// </summary>
        public ALPHABLEND_TYPE      Alphablend;
        
        /// <summary>
        /// トランスフォーム行列
        /// </summary>
        internal Matrix4            m_Transform;
        
        /// <summary>
        /// 描画オブジェクトID
        /// </summary>
        internal DRAW_OBJECT_ID     m_DrawObjectID;
    }
}
