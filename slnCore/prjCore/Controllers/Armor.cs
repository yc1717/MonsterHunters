using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using prjCore.Models;//使用MonsterHunterContext必須引用此命名空間
using System;
using System.Collections.Generic;//接收資料
using System.Linq;
using System.Threading.Tasks;


namespace prjCore.Controllers
{
    public class Armor : Controller
    {
        //建立MonsterHunterContext物件db
        MonsterHunterContext db = new MonsterHunterContext();
        public IActionResult Index()
            //連結/Armor/Index，會執行Index()方法
            //用來執行Index.cshtml檢視頁面，並將資料庫中的資料回傳至檢視頁面
        {
            var armors = db.Armor.ToList();//將傳回的資料轉成 List 資料型別
            return View(armors);//將資料回傳至檢視頁面
        }

        public IActionResult Create()//連結到Create頁面會執行此方法
        {
            return View();
        }

        [HttpPost]//利用Post方法接收資料
        //當在Create.cshtml按下"新增防具"的Submit按鈕會執行此方法
        public IActionResult Create(Models.Armor armor)//新增資料--透過Model取得使用者輸入資料
        {
            db.Armor.Add(armor);//將資料加進資料庫
            db.SaveChanges();//儲存變更
            return View();//回傳至檢視頁面
        }

        public IActionResult Edit(int id)//連結到Edit頁面會執行此方法
        {
            //取出要編輯的資料
            var armor = db.Armor.Where(m => m.Id == id).FirstOrDefault();
            return View(armor);//將資料回傳至檢視頁面
        }

        [HttpPost]//利用Post方法接收資料
        //當在Edit頁面按下"儲存"的Submit按鈕會執行此方法
        public IActionResult Edit(Models.Armor armor)//修改數據--透過Model取得使用者輸入資料
        {
            var modify = db.Armor.Where(m => m.Id == armor.Id).FirstOrDefault();//讀取修改數據
            modify.系列名稱 = armor.系列名稱;
            modify.腕甲 = armor.腕甲;
            modify.腰甲 = armor.腰甲;
            modify.護腿 = armor.護腿;
            modify.鎧甲 = armor.鎧甲;
            modify.頭盔 = armor.頭盔;
            db.SaveChanges();
            return RedirectToAction("Index");//導回防具列表
        }

        public IActionResult Delete(int id)////連結到Delete頁面會執行此方法
        {//刪除數據--透過Model取得使用者想刪除的資料
            var armor = db.Armor.Where(m => m.Id == id).FirstOrDefault();//刪除數據--透過Model取得使用者想刪除的資料
            return View(armor);//導回防具列表
        }

        [HttpPost]//利用Post方法接收資料
                  //當在Delete頁面按下"刪除防具"的Submit按鈕會執行此方法
        public IActionResult Delete(Models.Armor armor)//刪除數據
        {
            var result = db.Armor.Where(m => m.Id == armor.Id).FirstOrDefault();//讀取要刪除的資料
            db.Armor.Remove(result);//在資料庫中將資料移除
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]//利用Post方法接收資料
        //查詢方法
        public string ShowQuery(IFormCollection fc,string keyword)
        {
            MonsterHunterContext db = new MonsterHunterContext();//導入資料庫
            string show = "";
            var result = db.Armor.Where(i => i.系列名稱.Contains(keyword));//比對系列名稱欄位
            if (result.FirstOrDefault() == null)//如果找不到資料的話顯示查無此資料
            {
                show += "查無「"+keyword+ "」";
            }
            foreach (var i in result)//依序列出搜尋結果
            {
                show += $"系列名稱:{i.系列名稱}\n";
                show += $"頭盔:{i.頭盔}\n";
                show += $"鎧甲:{i.鎧甲}\n";
                show += $"腕甲:{i.腕甲}\n";
                show += $"腰甲:{i.腰甲}\n";
                show += $"護腿:{i.護腿}\n";
                show += "-----------------------------\n";

            }
            return show;

        }

        
        
    }
}
