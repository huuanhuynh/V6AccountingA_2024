using System;
using System.Linq;
using V6Soft.Models.Accounting.DTO;
using V6Soft.Models.Accounting.ViewModels.V6Option;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.System.Extensions
{
    public static class ModelConverter
    {
        public static V6Option ToV6OptionDto(this V6OptionDetail source)
        {
            return new V6Option
            {
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                CreatedTime = DateTime.Now.ToString("hh:mm:ss"),
                ModifiedTime = DateTime.Now.ToString("hh:mm:ss"),
                CreatedUserId = source.CreatedUserId,
                ModifiedUserId = source.ModifiedUserId,
               FormatLoai = source.FormatLoai,
               Loai = source.Loai,
               MaPhanHe = source.MaPhanHe,
               Name = source.Name,
               STT = source.STT,    
               Attribute = source.attribute,
               Defaul = source.defaul,
               Descript = source.descript,
               Descript2 = source.descript2,
               Inputmask = source.inputmask,
               Val = source.val,
                UID = source.UID,
            };
        }

        public static PagedSearchResult<V6OptionListItem> ToV6OptionViewModel(this PagedSearchResult<V6Option> source)
        {
            var customerItems = source.Data.Select(
                x => new V6OptionListItem
                {
                    Name = x.Name,
                    STT = x.STT,
                    descript2 = x.Descript2,
                    FormatLoai = x.FormatLoai,
                    Loai = x.Loai,
                    MaPhanHe = x.MaPhanHe,
                    UID = x.UID,
                    attribute = x.Attribute,
                    defaul = x.Defaul,
                    descript = x.Descript,
                    inputmask = x.Inputmask,
                    val = x.Val,
                }
            );
            return new PagedSearchResult<V6OptionListItem>(customerItems.ToList(), source.Total);
        }
    }
}
