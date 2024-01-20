using MSArchivosEscalafonDocente.Models;
using MSArchivosEscalafonDocente.Services;
using Microsoft.AspNetCore.Mvc;

namespace MSArchivosEscalafonDocente.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly FilesService _filesService;

    public FilesController(FilesService filesService) =>
        _filesService = filesService;

    [HttpGet]
    public async Task<List<FilesModel>> Get() => await _filesService.GetAsync();


    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<FilesModel>> Get(string id)
    {
        var file = await _filesService.GetAsync(id);

        if (file is null)
        {
            return NotFound();
        }
        return file;
    }

[HttpPost]
[Consumes("multipart/form-data")]
public async Task<IActionResult> Post([FromForm] FilesModel newFile)
{
    try
    {
        // Convierte el contenido del IFormFile a un array
        /*
        if (newFile.ArchivoParaCargar != null)
        {
            newFile.Documento = await convertToString(newFile.ArchivoParaCargar);
            newFile.ArchivoParaCargar = null;
        }
        */
        // Guardar el objeto en MongoDB
        await _filesService.CreateAsync(newFile);

        //return CreatedAtAction(nameof(Get), new { id = newFile.Id }, newFile);
        return Ok(new { id = newFile.Id });
    }
    catch (Exception ex)
    {
        return BadRequest($"Error al procesar la solicitud: {ex.Message}");
    }
}
    
    /*
    private async Task<string> convertToString(IFormFile file)
    {
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
    */
    

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, FilesModel updatedFile)
    {
        var file = await _filesService.GetAsync(id);

        if (file is null)
        {
            return NotFound();
        }

        updatedFile.Id = file.Id;

        await _filesService.UpdateAsync(id, updatedFile);

        //return NoContent();
        return Ok(new { Message = "El documento fue modificado correctamente." });        
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var file = await _filesService.GetAsync(id);

        if (file is null)
        {
            return NotFound();
        }

        await _filesService.RemoveAsync(id);

        //return NoContent();
        return Ok(new { Message = "Documento eliminado correctamente." });

    }
}