namespace SototiSite.App_Start
{
    using System;
    using System.Web.Mvc;

    public class CustomModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            return SimpleInjectorInitializer.Container.GetRegistration(modelType) != null ? new CustomModelBinder() : null;
        }
    }
}