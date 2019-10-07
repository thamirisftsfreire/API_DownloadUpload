using GEA_Application;
using GEA_Domain.Entities;
using GEA_Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace GEA_API.Controllers
{
    public class ArquivoController : ApiController
    {
        private readonly IArquivoAppService _arquivoAppService;
        private Arquivo _arquivo;
        public ArquivoController(IArquivoAppService arquivoAppService, Arquivo arquivo)
        {
            _arquivoAppService = arquivoAppService;
            _arquivo = arquivo;
        }
        [HttpPost]
        public HttpResponseMessage UploadArquivoBD()
        {
            HttpResponseMessage result = null;
            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    var postedFile = httpRequest.Files[0];
                    var caminhoDestino = httpRequest.Params["caminhoDestino"];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    using (var binaryReader = new BinaryReader(httpRequest.Files[0].InputStream))
                    {                       
                        _arquivo.ImagemArquivo = binaryReader.ReadBytes(httpRequest.Files[0].ContentLength); 
                    }
                    _arquivo.CaminhoArquivo = caminhoDestino;
                    _arquivo.NomeArquivo = httpRequest.Files[0].FileName;
                    _arquivoAppService.Adicionar(_arquivo);
                    result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, ex.Message);
            }

        }
       
        [HttpPost]
        public HttpResponseMessage UploadArquivoFileServer()
        {
            HttpResponseMessage result = null;
            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    var postedFile = httpRequest.Files[0];
                    var caminhoDestino = httpRequest.Params["caminhoDestino"];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    using (var binaryReader = new BinaryReader(httpRequest.Files[0].InputStream))
                    {
                        _arquivo.ImagemArquivo = binaryReader.ReadBytes(httpRequest.Files[0].ContentLength);
                    }
                    _arquivo.CaminhoArquivo = caminhoDestino;
                    _arquivo.NomeArquivo = httpRequest.Files[0].FileName;
                    _arquivoAppService.AdicionarArquivoFileServer(_arquivo);
                    result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, ex.Message);
            }
        }

        
        [HttpGet]
        [ResponseType(typeof(Byte[]))]
        public HttpResponseMessage DownloadArquivoBD(string nomeArquivo)
        {
            HttpResponseMessage result = null;
            try
            {
                _arquivo.NomeArquivo = nomeArquivo;
                var file = _arquivoAppService.ObterArquivo(_arquivo);

                if (file == null)
                {
                    result = Request.CreateResponse(HttpStatusCode.Gone);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(file.ImagemArquivo);
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = file.NomeArquivo;
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(file.NomeArquivo));
                }

                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, ex.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(Byte[]))]
        public HttpResponseMessage DownloadArquivoFileServer(string caminhoOrigem, string nomeArquivo)
        {
            HttpResponseMessage result = null;
            try
            {
                _arquivo.CaminhoArquivo = HttpContext.Current.Server.UrlDecode(caminhoOrigem);
                _arquivo.NomeArquivo = nomeArquivo;
                var file = _arquivoAppService.ObterArquivoFileServer(_arquivo);

                if (file == null)
                {
                    result = Request.CreateResponse(HttpStatusCode.Gone);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.OK);
                    result.Content = new ByteArrayContent(file.ImagemArquivo);
                    result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentDisposition.FileName = file.NomeArquivo;
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(file.NomeArquivo));
                }

                return result;
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, ex.Message);
            }
        }
    }
}
