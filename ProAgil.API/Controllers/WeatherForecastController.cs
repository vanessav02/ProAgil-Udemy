using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ProAgil.API.Model;
using ProAgil.API.Data;


namespace ProAgil.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public readonly DataContext _context; // propriedade readonly é iagual a: public DataContext Context { get; }
        public WeatherForecastController(DataContext context)
        {
            _context = context;

        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get() //chamada abrindo uma thread Task
        {
            try
            {
                var results = await _context.Eventos.ToListAsync(); //para cada uma das threads abertas vai ter uma espera await, não travando o recurso 

                return Ok(results); //ok retorna status code 200
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

        // GET api/values/2 //se for na rota do id, filtra qual escolheu
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var results = await _context.Eventos.FirstOrDefaultAsync(x => x.EventoId == id); 

                return Ok(results); //ok retorna status code 200
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Banco de Dados Falhou");
            }
        }

    }
}
