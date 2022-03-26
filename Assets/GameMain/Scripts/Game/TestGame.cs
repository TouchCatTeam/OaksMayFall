// ----------------------------------------------
// 作者: 廉价喵
// 创建于: 12/03/2022 15:05
// 最后一次修改于: 26/03/2022 8:25
// 版权所有: ThinkDifferentStudio
// 描述:
// ----------------------------------------------

namespace OaksMayFall
{
    public class TestGame: GameBase
    {
        private float m_ElapseSeconds = 0f;

        public override GameMode GameMode
        {
            get
            {
                return GameMode.Test;
            }
        }

        public override void Initialize()
        {
            base.Initialize();


        }
        
        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
        }
    }
}
