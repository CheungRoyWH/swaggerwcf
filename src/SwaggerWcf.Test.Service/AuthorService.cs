﻿using SwaggerWcf.Attributes;
using SwaggerWcf.Test.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SwaggerWcf.Test.Service
{
    [SwaggerWcf("/v1/authors")]
    [SwaggerWcfTag("Authors")]
    [SwaggerWcfServiceInfo(
        title: "SampleAuthorService",
        version: "0.0.1",
        Description = "Sample Author Service to test SwaggerWCF",
        TermsOfService = "Terms of Service"
    )]
    [SwaggerWcfContactInfo(
        Name = "Abel Silva",
        Url = "http://github.com/abelsilva",
        Email = "no@e.mail"
    )]
    [SwaggerWcfLicenseInfo(
        name: "Apache License 2.0",
        Url = "https://github.com/abelsilva/SwaggerWCF/blob/master/LICENSE"
    )]
    public class AuthorService : BaseCRUDService<Author>, IAuthorService
    {
        public override Author Get(string id)
        {
            var obj = base.Get(id);
            obj.Locales = new List<LocaleModel>
            {
                new LocaleModel {  Language = LocaleLanguage.En, Key = "Name", Value = "Value(EN)"},
                new LocaleModel {  Language = LocaleLanguage.ZhCn, Key = "Name", Value = "Value(CN)"},
            };

            return obj;
        }
    }
}