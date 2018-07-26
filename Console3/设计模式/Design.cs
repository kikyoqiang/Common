using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console3
{
    /// <summary>
    /// 设计模式
    /// </summary>
    class Design
    {
        static void Main22()
        {
            #region MyRegion
            //Creator tuFactory = new TuDouSiFactory();
            //Creator xiFactory = new TomatoFractory();
            //Food2 tu = tuFactory.CteateFoodFactory();
            //Food2 xi = xiFactory.CteateFoodFactory();
            //tu.Print();
            //xi.Print(); 
            #endregion

            #region MyRegion
            //// 南昌工厂制作南昌的鸭脖和鸭架
            //AbstractFactory nanChangFactory = new NanChangFactory();
            //YaBo nanChangYabo = nanChangFactory.CreateYaBo();
            //nanChangYabo.Print();
            //YaJia nanChangYajia = nanChangFactory.CreateYaJia();
            //nanChangYajia.Print();

            //// 上海工厂制作上海的鸭脖和鸭架
            //AbstractFactory shangHaiFactory = new ShangHaiFactory();
            //shangHaiFactory.CreateYaBo().Print();
            //shangHaiFactory.CreateYaJia().Print(); 
            #endregion

            #region MyRegion
            //Director director = new Director();
            //Builder1 builder1 = new Builder1();
            //Builder2 builder2 = new Builder2();

            //director.Construct(builder1);
            //Computer c1 = builder1.GetComputer();
            //c1.Show();

            //director.Construct(builder2);
            //Computer c2 = builder2.GetComputer();
            //c2.Show(); 
            #endregion

            #region MyRegion
            //MonkeyKingPrototype conCreatePrototype = new ConCreatePrototype("MonkeyKing");

            //MonkeyKingPrototype clone1 = conCreatePrototype.Clone();
            //Console.WriteLine(clone1.Id);

            //MonkeyKingPrototype clone2 = conCreatePrototype.Clone();
            //Console.WriteLine(clone2.Id); 
            #endregion

            #region 适配器模式
            //IThreeHole threeHole = new PowerAdapter();
            //threeHole.Request();

            //PowerAdapter2 threeHole2 = new PowerAdapter2();
            //threeHole2.Request(); 
            #endregion

            #region 桥接模式
            //RemoteControl remoteControl = new RemoteControl();
            //remoteControl.Implementor = new ChangHong();
            //remoteControl.On();
            //remoteControl.SetChannel();
            //remoteControl.Off();
            //Console.WriteLine();

            //ConcreateRemote remoteControl2 = new ConcreateRemote();
            //remoteControl2.Implementor = new Samsung();
            //remoteControl2.On();
            //remoteControl2.SetChannel();
            //remoteControl2.Off();
            //Console.WriteLine();
            #endregion

            #region 装饰者模式
            //Phone phone = new ApplePhone();
            //Sticker stickerPhone = new Sticker(phone);
            ////stickerPhone.Print();

            //accessories aPhone = new accessories(stickerPhone);
            //aPhone.Print(); 
            #endregion

            #region 组合模式
            //ComplexGraphics complexGraphics = new ComplexGraphics("一个复杂图形和两条线段组成的复杂图形");
            //complexGraphics.Add(new Line("线段A"));

            //ComplexGraphics compositeCG = new ComplexGraphics("一个圆和一条线组成的复杂图形");
            //compositeCG.Add(new Line("线段B"));
            //compositeCG.Add(new Circle("圆圈"));

            //complexGraphics.Add(compositeCG);

            //Line l = new Line("线段C");
            //complexGraphics.Add(l);

            //// 显示复杂图形的画法
            //Console.WriteLine("复杂图形的绘制如下：");
            //Console.WriteLine("---------------------");
            //complexGraphics.Draw();
            //Console.WriteLine("复杂图形绘制完成");
            //Console.WriteLine("---------------------");
            //Console.WriteLine();

            //// 移除一个组件再显示复杂图形的画法
            //complexGraphics.Remove(l);
            //Console.WriteLine("移除线段C后，复杂图形的绘制如下：");
            //Console.WriteLine("---------------------");
            //complexGraphics.Draw();
            //Console.WriteLine("复杂图形绘制完成");
            //Console.WriteLine("---------------------"); 
            #endregion

            Console.ReadKey();
        }
    }
}
