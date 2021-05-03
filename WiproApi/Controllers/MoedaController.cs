using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using WiproApi.Models;

namespace WiproApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoedaController : ControllerBase
    {
        Stack<DadosMoeda> Fila;
         
        [HttpPost]
        public void AddItemFila(Stack<DadosMoeda> dados)
        {
            Fila = new Stack<DadosMoeda>();

            foreach (var item in dados)
            {
                Fila.Push(item);
            }
           
            HttpContext.Session.SetString("SessionFila", JsonConvert.SerializeObject(Fila));
        }

        [HttpGet]
        public IActionResult GetItemFila()
        {
            Fila  = JsonConvert.DeserializeObject<Stack<DadosMoeda>> 
              (HttpContext.Session.GetString("SessionFila"));

            if (Fila.Any())
            {
                var ultimoFila = Fila.Peek();
                Fila.Pop();

               
                HttpContext.Session.SetString("SessionFila", JsonConvert.SerializeObject(Fila));

                return Ok(ultimoFila);
            }

            return NotFound("Fila Vazia");
        }
    }
}
