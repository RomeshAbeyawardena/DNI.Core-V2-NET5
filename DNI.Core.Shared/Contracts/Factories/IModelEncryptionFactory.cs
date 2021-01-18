using DNI.Core.Shared.Contracts.Services;
using System;
using System.Collections.Generic;

namespace DNI.Core.Shared.Contracts.Factories
{
    /// <summary>
    /// Represents a model encryption factory for getting a model encryption service used for encrypting and decrypting entire models at a time
    /// </summary>
    public interface IModelEncryptionFactory
    {
        /// <summary>
        /// Retrieves the model encryption service to use
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IModelEncryptionService<T> GetModelEncryptionService<T>();

        /// <summary>
        /// Encrypts a model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        void Encrypt<T>(T model);

        /// <summary>
        /// Decrypts a model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        void Decrypt<T>(T model);

        /// <summary>
        /// Encrypts a list of models
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        void Encrypt<T>(IEnumerable<T> model);

        /// <summary>
        /// Decrypts a list of models
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        void Decrypt<T>(IEnumerable<T> model);

        /// <summary>
        /// Encrypts a model within a model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="model"></param>
        /// <param name="propertySelector"></param>
        void Encrypt<T, TSelector>(T model, Func<T, TSelector> propertySelector);

        /// <summary>
        /// Decrypts a model within a model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="model"></param>
        /// <param name="propertySelector"></param>
        void Decrypt<T, TSelector>(T model, Func<T, TSelector> propertySelector);

        
        /// <summary>
        /// Encrypts a list of models within a model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="model"></param>
        /// <param name="propertySelector"></param>
        void Encrypt<T, TSelector>(T model, Func<T, IEnumerable<TSelector>> propertySelector);

        
        /// <summary>
        /// Encrypts a collection of models within a model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="model"></param>
        /// <param name="propertySelector"></param>
        void Encrypt<T, TSelector>(T model, Func<T, ICollection<TSelector>> propertySelector);

        
        /// <summary>
        /// Decrpts a list of models within a model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="model"></param>
        /// <param name="propertySelector"></param>
        void Decrypt<T, TSelector>(T model, Func<T, IEnumerable<TSelector>> propertySelector);

        /// <summary>
        /// Decrpts a list of models within a model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TSelector"></typeparam>
        /// <param name="model"></param>
        /// <param name="propertySelector"></param>
        void Decrypt<T, TSelector>(T model, Func<T, ICollection<TSelector>> propertySelector);
        
        /// <summary>
        /// Decrypts a model in a new instance of <typeparamref name="T"/>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        T DecryptAsClone<T>(T model, params object[] arguments);

        /// <summary>
        /// Encrypts a model in a new instance of <typeparamref name="T"/>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        T EncryptAsClone<T>(T model, params object[] arguments);
    }
}
