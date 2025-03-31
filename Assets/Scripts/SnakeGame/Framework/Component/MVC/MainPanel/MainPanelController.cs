using UnityEngine;

public class MainPanelController : IComponent
{
    MainPanelModel model;
    MainPanelView view;
    public MainPanelController(ComponentType type, IGameObject obj) : base(type, obj)
    {
        model = new MainPanelModel(obj);
        model.Initialize();
        view = new MainPanelView(obj);
        view.Initialize();
    }

    public override void Initialize()
    {
        //???????????
        BindNetEvent();
        //????UI???
        BindUIEvent();
        //????γ????
        UpdateItem();
        
    }

    public void BindUIEvent()
    {
        view.ShowMainMoney(model.data.money);
        view.ShowMainDiamond(model.data.diamond);
        //view.btn_start.onClick.AddListener(() => 
        //{
        //    //??????
        //    Debug.Log("??????");
        //});
    }

    public void BindNetEvent()
    {
        MessageManager.AddListener(CMD.Child, (string num) =>
        {
            int inpex = int.Parse(num)-1;
            model.AddLevel(inpex);
        });
        MessageManager.AddListener(CMD.ShowLevel,()=>
        {
            UpdateItem();
            view.ShowMainMoney(model.data.money);
        });
        MessageManager.AddListener(CMD.ShowDiamond,()=>
        {
            view.ShowMainDiamond(model.data.diamond);
        });
        MessageManager.AddListener(CMD.ShowMonwy,()=>
        {
            model.ShowMoney();
            view.ShowMainMoney(model.data.money);
        });
        MessageManager.AddListener(CMD.UpdataPlayerLevel,()=>{
            //调用更新的事件
        });
    }

    public override void Update()
    {
        base.Update();

    }

    public void UpdateItem()
    {
        for (int i = 0; i < model.items.Count; i++)
        {
            view.items[i].GetComponent<MainPanelItem>().Init(model.items[i],model.data.money);
        }
    }
}
