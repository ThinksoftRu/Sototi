namespace SototiSite.App_Start
{
    using System;
    using System.Web.Mvc;

    public class CustomModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(
            ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            return SimpleInjectorInitializer.Container.GetInstance(modelType);
        }
    }
}