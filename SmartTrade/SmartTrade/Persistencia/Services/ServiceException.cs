using System;

namespace SmartTrade.Persistencia.Services
{


    public class ServiceException : Exception
    {
        public ServiceException()
        {
        }

        public ServiceException(string message)
        : base(message)
        {
        }

        public ServiceException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }

    public class UserNotFoundException : ServiceException
    {
        public UserNotFoundException() : base("El usuario introducido no existe") { }
        public UserNotFoundException(string message) : base("El usuario " + message + " no ha sido econtrado.") { }
        public UserNotFoundException(string message, Exception inner) : base(message, inner) { }
    }

   
    public class IncorrectPasswordException : ServiceException
    {
        public IncorrectPasswordException() : base("Contraseña incorrecta, inténtelo de nuevo") { }
        public IncorrectPasswordException(string message) : base(message)
        {
        }

    }


    public class EmailYaRegistradoException : ServiceException
    {
        public EmailYaRegistradoException() : base("Este correo ya ha sido registrado") { }
        public EmailYaRegistradoException(string message) : base(message)
        {
        }
    }
    public class EmailFormatoIncorrectoException : ServiceException
    {
        public EmailFormatoIncorrectoException() : base("El correo no tiene formato adecuado") { }
        public EmailFormatoIncorrectoException(string message) : base(message)
        {
        }
    }
    public class NickYaRegistradoException : ServiceException
    {
        public NickYaRegistradoException() : base("Este Nick ya ha sido registrado") { }
        public NickYaRegistradoException(string message) : base(message)
        {
        }
    }
}