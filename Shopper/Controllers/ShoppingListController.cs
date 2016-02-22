using Shopper.Auth;
using Shopper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shopper.Controllers
{
    [CustAuthorize]
    public class ShoppingListController : ApiController
    {
        // GET: api/ShoppingList/all
        [Route("api/ShoppingList/all")]
        public IHttpActionResult Get(int Offset=1, int limit=3)
        {
            var all = ShoppingList.Items;
            var count = all.Count;
            var qry = all.OrderBy(p=>p.Item.Id);
            var items = qry.Skip(Offset - 1).Take(limit).ToList();
            return Ok(new MyPagedList<ShoppingItem>() { Count = count, Items= items, Offset=Offset, Limit= 3 });
        }

        // GET: api/ShoppingList/5
        [Route("api/ShoppingList/{id}",Name="GetItemById")]
        public IHttpActionResult Get(int id)
        {
            var itm = ShoppingList.Items.FirstOrDefault(p => p.Item.Id == id);
            if (itm == null)
                return NotFound();
            
            return Ok(itm);
        }

        [Route("api/ShoppingList/{name}")]
        public IHttpActionResult Get(string name)
        {
            var itm = ShoppingList.Items.FirstOrDefault(p => p.Item.Name == name);
            if (itm == null)
                return NotFound();

            return Ok(itm);
        }


        // POST: api/ShoppingList/add
        [Route("api/ShoppingList/add")]
        public IHttpActionResult Post(ShoppingItem value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = ShoppingList.Add(value.Item, value.Quantity);
            if (res)
                return CreatedAtRoute("GetItemById", new { id = value.Item.Id }, value);
            return BadRequest("Could not add to shopping list");
        }

        // PUT: api/ShoppingList/5
        [Route("api/ShoppingList/{name}")]
        public IHttpActionResult Put(string name, ShoppingItem shoppingItem)
        {
            if (!ShoppingList.ExistsInList(name))
                return NotFound();
            if (name != shoppingItem.Item.Name)
                return BadRequest("Wrong Item");
            var res = ShoppingList.UpdateQuantity(name, shoppingItem.Quantity);
            if (res)
                return Ok(shoppingItem);
            return BadRequest("Could not update item");
        }

        // DELETE: api/ShoppingList/5
        [Route("api/ShoppingList/{name}")]
        public IHttpActionResult Delete(string name)
        {
            if (!ShoppingList.ExistsInList(name))
                return NotFound();
            var res = ShoppingList.Delete(name);
            if (res)
                return Ok();
            return BadRequest("Could not remove item");
        }
    }
}
