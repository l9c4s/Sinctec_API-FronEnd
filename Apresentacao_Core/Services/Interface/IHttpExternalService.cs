namespace Apresentacao_Core.Services.Interface
{
    public interface IHttpExternalService
    {
        public Task<object> CriarTokenIdentity(object jObject);

        public Task<object> AdicionatUsarioIdentity(object JOject);

        public Task<object> ConfirmarEmailIdentity(object JOject);

        public Task<object> EmailResetDeSenha(string EmailUser);

        public Task<object> ResetarSenhaIdentity(object JOject);
    }
}
