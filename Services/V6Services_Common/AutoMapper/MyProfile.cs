using AutoMapper;

namespace V6Soft.Services.Common.AutoMapper
{
    public class MyProfile : Profile
    {
        public const string VIEW_MODEL = "MyProfileNameHere";
        protected string ProfileName
        {
            get { return VIEW_MODEL; }
        }

        protected override void Configure()
        {
            CreateMaps();
        }

        private static void CreateMaps()
        {
            //Create Maps here…we'll get to this in a second
        }
    }
}
