﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using WebVella.ERP.Storage;
using WebVella.ERP.Api.Models;
using WebVella.ERP.Api;
using Newtonsoft.Json.Linq;
using WebVella.ERP.Utilities.Dynamic;

namespace WebVella.ERP
{
    public class ERPService : IERPService
    {
        public static IERPService Current
        {
            get; set;
        }

        public IStorageService StorageService
        {
            get; set;
        }

        public ERPService(IStorageService storage)
        {
            if (Current == null)
                Current = this;

            StorageService = storage;
        }

        public void RunTests()
        {
            InitializeSystemEntities();
            //EntityTests();
        }

        public void InitializeSystemEntities()
        {
            IStorageSystemSettingsRepository systemSettingsRepository = StorageService.GetSystemSettingsRepository();
            IStorageSystemSettings storeSystemSettings = systemSettingsRepository.Read();

            Guid systemSettingsId = new Guid("F3223177-B2FF-43F5-9A4B-FF16FC67D186");
            SystemSettings systemSettings = new SystemSettings();
            systemSettings.Id = systemSettingsId;

            int currentVersion = 0;
            if (storeSystemSettings != null)
            {
                systemSettings = new SystemSettings(storeSystemSettings);
                currentVersion = systemSettings.Version;
            }

            EntityManager entityManager = new EntityManager(StorageService);

            if (currentVersion < 150508)
            {
                systemSettings.Version = 150508;

                List<Guid> allowedRoles = new List<Guid>();
                allowedRoles.Add(new Guid("F42EBA3B-6433-752B-6C34-B322A7B4CE7D"));


                InputEntity roleEntity = new InputEntity();
                roleEntity.Id = SystemIds.RoleEntityId;
                roleEntity.Name = "role";
                roleEntity.Label = "Role";
                roleEntity.PluralLabel = "Roles";
                roleEntity.System = true;
                roleEntity.RecordPermissions = new RecordPermissions();
                roleEntity.RecordPermissions.CanRead = allowedRoles;
                roleEntity.RecordPermissions.CanCreate = allowedRoles;
                roleEntity.RecordPermissions.CanUpdate = allowedRoles;
                roleEntity.RecordPermissions.CanDelete = allowedRoles;

                EntityResponse response = entityManager.CreateEntity(roleEntity);

                TextField nameRoleField = new TextField();

                nameRoleField.Id = new Guid("36F91EBD-5A02-4032-8498-B7F716F6A349");
                nameRoleField.Name = "name";
                nameRoleField.Label = "Name";
                nameRoleField.PlaceholderText = "";
                nameRoleField.Description = "The name of the role";
                nameRoleField.HelpText = "";
                nameRoleField.Required = true;
                nameRoleField.Unique = false;
                nameRoleField.Searchable = true;
                nameRoleField.Auditable = false;
                nameRoleField.System = true;
                nameRoleField.DefaultValue = "";

                nameRoleField.MaxLength = 200;

                FieldResponse fieldResponse = entityManager.CreateField(roleEntity.Id.Value, nameRoleField);

                TextField descriptionRoleField = new TextField();

                descriptionRoleField.Id = new Guid("4A8B9E0A-1C36-40C6-972B-B19E2B5D265B");
                descriptionRoleField.Name = "description";
                descriptionRoleField.Label = "Description";
                descriptionRoleField.PlaceholderText = "";
                descriptionRoleField.Description = "";
                descriptionRoleField.HelpText = "";
                descriptionRoleField.Required = true;
                descriptionRoleField.Unique = false;
                descriptionRoleField.Searchable = true;
                descriptionRoleField.Auditable = false;
                descriptionRoleField.System = true;
                descriptionRoleField.DefaultValue = "";

                descriptionRoleField.MaxLength = 200;

                fieldResponse = entityManager.CreateField(roleEntity.Id.Value, descriptionRoleField);


                InputEntity userEntity = new InputEntity();
                userEntity.Id = SystemIds.UserEntityId;
                userEntity.Name = "user";
                userEntity.Label = "User";
                userEntity.PluralLabel = "Users";
                userEntity.System = true;
                userEntity.RecordPermissions = new RecordPermissions();
                userEntity.RecordPermissions.CanRead = allowedRoles;
                userEntity.RecordPermissions.CanCreate = allowedRoles;
                userEntity.RecordPermissions.CanUpdate = allowedRoles;
                userEntity.RecordPermissions.CanDelete = allowedRoles;

                response = entityManager.CreateEntity(userEntity);

                TextField firstName = new TextField();

                firstName.Id = new Guid("DF211549-41CC-4D11-BB43-DACA4C164411");
                firstName.Name = "first_name";
                firstName.Label = "First Name";
                firstName.PlaceholderText = "";
                firstName.Description = "First name of the user";
                firstName.HelpText = "";
                firstName.Required = true;
                firstName.Unique = false;
                firstName.Searchable = true;
                firstName.Auditable = false;
                firstName.System = true;
                firstName.DefaultValue = "";

                firstName.MaxLength = 200;

                fieldResponse = entityManager.CreateField(userEntity.Id.Value, firstName);

                TextField lastName = new TextField();

                lastName.Id = new Guid("63E685B1-B2C6-4961-B393-2B6723EBD1BF");
                lastName.Name = "last_name";
                lastName.Label = "Last Name";
                lastName.PlaceholderText = "";
                lastName.Description = "Last name of the user";
                lastName.HelpText = "";
                lastName.Required = true;
                lastName.Unique = false;
                lastName.Searchable = true;
                lastName.Auditable = false;
                lastName.System = true;
                lastName.DefaultValue = "";

                lastName.MaxLength = 200;

                fieldResponse = entityManager.CreateField(userEntity.Id.Value, lastName);

                EmailField email = new EmailField();

                email.Id = new Guid("9FC75C8F-CE80-4A64-81D7-E2BEFA5E4815");
                email.Name = "email";
                email.Label = "Email";
                email.PlaceholderText = "";
                email.Description = "Email address of the user";
                email.HelpText = "";
                email.Required = true;
                email.Unique = true;
                email.Searchable = true;
                email.Auditable = false;
                email.System = true;
                email.DefaultValue = "";

                email.MaxLength = 255;

                fieldResponse = entityManager.CreateField(userEntity.Id.Value, email);

                PasswordField password = new PasswordField();

                password.Id = new Guid("4EDE88D9-217A-4462-9300-EA0D6AFCDCEA");
                password.Name = "password";
                password.Label = "Password";
                password.PlaceholderText = "";
                password.Description = "Password for the user account";
                password.HelpText = "";
                password.Required = true;
                password.Unique = true;
                password.Searchable = true;
                password.Auditable = false;
                password.System = true;

                password.MaxLength = 1;
                password.MaskType = Api.PasswordFieldMaskTypes.MaskAllCharacters;
                password.MaskCharacter = '*';
                password.Encrypted = true;

                fieldResponse = entityManager.CreateField(userEntity.Id.Value, password);

                DateTimeField lastLoggedIn = new DateTimeField();

                lastLoggedIn.Id = new Guid("3C85CCEC-D526-4E47-887F-EE169D1F508D");
                lastLoggedIn.Name = "last_logged_in";
                lastLoggedIn.Label = "Last Logged In";
                lastLoggedIn.PlaceholderText = "";
                lastLoggedIn.Description = "";
                lastLoggedIn.HelpText = "";
                lastLoggedIn.Required = false;
                lastLoggedIn.Unique = true;
                lastLoggedIn.Searchable = true;
                lastLoggedIn.Auditable = true;
                lastLoggedIn.System = true;
                lastLoggedIn.DefaultValue = null;

                lastLoggedIn.Format = "MM/dd/YYYY";
                lastLoggedIn.UseCurrentTimeAsDefaultValue = true;

                fieldResponse = entityManager.CreateField(userEntity.Id.Value, lastLoggedIn);

                CheckboxField enabledField = new CheckboxField();

                enabledField.Id = new Guid("C0C63650-7572-4252-8E4B-4E25C94897A6");
                enabledField.Name = "enabled";
                enabledField.Label = "Enabled";
                enabledField.PlaceholderText = "";
                enabledField.Description = "Shows if the user account is enabled";
                enabledField.HelpText = "";
                enabledField.Required = true;
                enabledField.Unique = false;
                enabledField.Searchable = true;
                enabledField.Auditable = false;
                enabledField.System = true;
                enabledField.DefaultValue = false;

                fieldResponse = entityManager.CreateField(userEntity.Id.Value, enabledField);

                CheckboxField verifiedUserField = new CheckboxField();

                verifiedUserField.Id = new Guid("F1BA5069-8CC9-4E66-BCC3-60E33C79C265");
                verifiedUserField.Name = "verified";
                verifiedUserField.Label = "Verified";
                verifiedUserField.PlaceholderText = "";
                verifiedUserField.Description = "Shows if the user email is verified";
                verifiedUserField.HelpText = "";
                verifiedUserField.Required = true;
                verifiedUserField.Unique = false;
                verifiedUserField.Searchable = true;
                verifiedUserField.Auditable = false;
                verifiedUserField.System = true;
                verifiedUserField.DefaultValue = false;

                fieldResponse = entityManager.CreateField(userEntity.Id.Value, verifiedUserField);

                storeSystemSettings = systemSettingsRepository.Convert(systemSettings);
                systemSettingsRepository.Save(storeSystemSettings);
            }

            //if (currentVersion == 150508) //update to 150510
            //{
            //    systemSettings.Version = 150510;

            //    storeSystemSettings = systemSettingsRepository.Convert(systemSettings);
            //    systemSettingsRepository.Save(storeSystemSettings);
            //}
        }

        private void EntityTests()
        {
            Debug.WriteLine("==== START ENTITY TESTS====");


            var entityManager = new EntityManager(StorageService);

            InputEntity inputEntity = new InputEntity();
            //entity.Id = new Guid("C5050AC8-5967-4CE1-95E7-A79B054F9D14");
            inputEntity.Id = Guid.NewGuid();
            inputEntity.Name = "goro_test";
            inputEntity.Label = "Goro Test";
            inputEntity.PluralLabel = "Goro Tests";
            inputEntity.System = true;

            List<Guid> allowedRoles = new List<Guid>();
            allowedRoles.Add(new Guid("F42EBA3B-6433-752B-6C34-B322A7B4CE7D"));
            inputEntity.RecordPermissions = new RecordPermissions();
            inputEntity.RecordPermissions.CanRead = allowedRoles;
            inputEntity.RecordPermissions.CanCreate = allowedRoles;
            inputEntity.RecordPermissions.CanUpdate = allowedRoles;
            inputEntity.RecordPermissions.CanDelete = allowedRoles;

            try
            {
                Entity entity = new Entity(inputEntity);

                EntityResponse response = entityManager.CreateEntity(inputEntity);

                TextField field = new TextField();
                field.Id = Guid.NewGuid();
                field.Name = "text_field";
                field.Label = "Text field";
                field.PlaceholderText = "Text field placeholder text";
                field.Description = "Text field description";
                field.HelpText = "Text field help text";
                field.Required = true;
                field.Unique = true;
                field.Searchable = true;
                field.Auditable = true;
                field.System = true;
                field.DefaultValue = "";

                field.MaxLength = 200;

                FieldResponse fieldResponse = entityManager.CreateField(entity.Id.Value, field);

                //inputEntity.Label = "GoroTest_edited";
                //inputEntity.PluralLabel = "Goro Tests - edited";

                //Expando obj = new Expando();
                //obj["Label"] = "GoroTest_edited";
                //obj["PluralLabel"] = "Goro Tests - edited";

                //response = entityManager.PartialUpdateEntity(entity.Id.Value, obj);

                //field.Label = "TextField_edited";

                Field fieldObj = new TextField();
                fieldObj.Label = "TextField_edited";

                fieldResponse = entityManager.PartialUpdateField(entity.Id.Value, field.Id.Value, fieldObj);

                //fieldResponse = entityManager.DeleteField(entity.Id.Value, field.Id.Value);

                //List<Field> fields = CreateTestFieldCollection(entity);
                ////FieldResponse fieldResponse = entityManager.CreateField(entity.Id.Value, fields[0]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[1]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[2]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[3]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[4]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[5]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[6]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[7]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[8]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[9]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[10]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[11]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[12]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[13]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[14]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[15]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[16]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[17]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[18]);
                //fieldResponse = entityManager.CreateField(entity.Id.Value, fields[19]);

                //EntityResponse entityResponse = entityManager.ReadEntity(entity.Id.Value);
                //entity = entityResponse.Object;

                //List<RecordsList> recordsLists = CreateTestViewCollection(entity);

                //RecordsListResponse recordsListsResponse = entityManager.CreateRecordsList(entity.Id.Value, recordsLists[0]);

                //recordsLists[0].Label = "Edited View";

                //recordsListsResponse = entityManager.UpdateRecordsList(entity.Id.Value, recordsLists[0]);

                //List<RecordView> recordViewList = CreateTestFormCollection(entity);

                //RecordViewResponse recordViewResponse = entityManager.CreateRecordView(entity.Id.Value, recordViewList[0]);

                //recordViewList[0].Label = "Edited Form";

                //recordViewResponse = entityManager.CreateRecordView(entity.Id.Value, recordViewList[0]);

                EntityListResponse entityListResponse = entityManager.ReadEntities();

                EntityResponse resultEntity = entityManager.ReadEntity(entity.Id.Value);

                response = entityManager.DeleteEntity(entity.Id.Value);

            }
            catch (StorageException e)
            {
                Debug.WriteLine(e);
            }

            Debug.WriteLine("==== END ENTITY TESTS====");
        }

        private List<Field> CreateTestFieldCollection(Entity entity)
        {
            List<Field> fields = new List<Field>();

            AutoNumberField autoNumberField = new AutoNumberField();

            autoNumberField.Id = Guid.NewGuid();
            autoNumberField.Name = "AutoNumberField";
            autoNumberField.Label = "AutoNumber field";
            autoNumberField.PlaceholderText = "AutoNumber field placeholder text";
            autoNumberField.Description = "AutoNumber field description";
            autoNumberField.HelpText = "AutoNumber field help text";
            autoNumberField.Required = true;
            autoNumberField.Unique = true;
            autoNumberField.Searchable = true;
            autoNumberField.Auditable = true;
            autoNumberField.System = true;
            autoNumberField.DefaultValue = 0;

            autoNumberField.DisplayFormat = "A{0000}";
            autoNumberField.StartingNumber = 10;

            fields.Add(autoNumberField);

            CheckboxField checkboxField = new CheckboxField();

            checkboxField.Id = Guid.NewGuid();
            checkboxField.Name = "CheckboxField";
            checkboxField.Label = "Checkbox field";
            checkboxField.PlaceholderText = "Checkbox field placeholder text";
            checkboxField.Description = "Checkbox field description";
            checkboxField.HelpText = "Checkbox field help text";
            checkboxField.Required = true;
            checkboxField.Unique = true;
            checkboxField.Searchable = true;
            checkboxField.Auditable = true;
            checkboxField.System = true;
            checkboxField.DefaultValue = false;

            fields.Add(checkboxField);

            CurrencyField currencyField = new CurrencyField();

            currencyField.Id = Guid.NewGuid();
            currencyField.Name = "CurrencyField";
            currencyField.Label = "Currency field";
            currencyField.PlaceholderText = "Currency field placeholder text";
            currencyField.Description = "Currency field description";
            currencyField.HelpText = "Currency field help text";
            currencyField.Required = true;
            currencyField.Unique = true;
            currencyField.Searchable = true;
            currencyField.Auditable = true;
            currencyField.System = true;
            currencyField.DefaultValue = 0;

            currencyField.MinValue = 1;
            currencyField.MaxValue = 35;
            currencyField.Currency = new CurrencyTypes();
            currencyField.Currency.CurrencyName = "USD";
            currencyField.Currency.CurrencySymbol = "$";
            currencyField.Currency.Position = CurrencyPosition.AfterTheNumber;

            fields.Add(currencyField);

            DateField dateField = new DateField();

            dateField.Id = Guid.NewGuid();
            dateField.Name = "DateField";
            dateField.Label = "Date field";
            dateField.PlaceholderText = "Date field placeholder text";
            dateField.Description = "Date field description";
            dateField.HelpText = "Date field help text";
            dateField.Required = true;
            dateField.Unique = true;
            dateField.Searchable = true;
            dateField.Auditable = true;
            dateField.System = true;
            dateField.DefaultValue = DateTime.MinValue;

            dateField.Format = "dd.MM.YYYY";

            fields.Add(dateField);

            DateTimeField dateTimeField = new DateTimeField();

            dateTimeField.Id = Guid.NewGuid();
            dateTimeField.Name = "DateTimeField";
            dateTimeField.Label = "DateTime field";
            dateTimeField.PlaceholderText = "DateTime field placeholder text";
            dateTimeField.Description = "DateTime field description";
            dateTimeField.HelpText = "DateTime field help text";
            dateTimeField.Required = true;
            dateTimeField.Unique = true;
            dateTimeField.Searchable = true;
            dateTimeField.Auditable = true;
            dateTimeField.System = true;
            dateTimeField.DefaultValue = DateTime.MinValue;

            dateTimeField.Format = "dd.MM.YYYY";

            fields.Add(dateTimeField);

            EmailField emailField = new EmailField();

            emailField.Id = Guid.NewGuid();
            emailField.Name = "EmailField";
            emailField.Label = "Email field";
            emailField.PlaceholderText = "Email field placeholder text";
            emailField.Description = "Email field description";
            emailField.HelpText = "Email field help text";
            emailField.Required = true;
            emailField.Unique = true;
            emailField.Searchable = true;
            emailField.Auditable = true;
            emailField.System = true;
            emailField.DefaultValue = "";

            emailField.MaxLength = 255;

            fields.Add(emailField);

            FileField fileField = new FileField();

            fileField.Id = Guid.NewGuid();
            fileField.Name = "FileField";
            fileField.Label = "File field";
            fileField.PlaceholderText = "File field placeholder text";
            fileField.Description = "File field description";
            fileField.HelpText = "File field help text";
            fileField.Required = true;
            fileField.Unique = true;
            fileField.Searchable = true;
            fileField.Auditable = true;
            fileField.System = true;
            fileField.DefaultValue = "";

            fields.Add(fileField);

            //FormulaField formulaField = new FormulaField();

            //formulaField.Id = Guid.NewGuid();
            //formulaField.Name = "Formula field";
            //formulaField.Label = "Formula field";
            //formulaField.PlaceholderText = "Formula field placeholder text";
            //formulaField.Description = "Formula field description";
            //formulaField.HelpText = "Formula field help text";
            //formulaField.Required = true;
            //formulaField.Unique = true;
            //formulaField.Searchable = true;
            //formulaField.Auditable = true;
            //formulaField.System = true;

            //formulaField.ReturnType = Api.FormulaFieldReturnType.Number;
            //formulaField.FormulaText = "2 + 5";
            //formulaField.DecimalPlaces = 2;

            //fields.Add(formulaField);

            HtmlField htmlField = new HtmlField();

            htmlField.Id = Guid.NewGuid();
            htmlField.Name = "HtmlField";
            htmlField.Label = "Html field";
            htmlField.PlaceholderText = "Html field placeholder text";
            htmlField.Description = "Html field description";
            htmlField.HelpText = "Html field help text";
            htmlField.Required = true;
            htmlField.Unique = true;
            htmlField.Searchable = true;
            htmlField.Auditable = true;
            htmlField.System = true;
            htmlField.DefaultValue = "";

            fields.Add(htmlField);

            ImageField imageField = new ImageField();

            imageField.Id = Guid.NewGuid();
            imageField.Name = "ImageField";
            imageField.Label = "Image field";
            imageField.PlaceholderText = "Image field placeholder text";
            imageField.Description = "Image field description";
            imageField.HelpText = "Image field help text";
            imageField.Required = true;
            imageField.Unique = true;
            imageField.Searchable = true;
            imageField.Auditable = true;
            imageField.System = true;
            imageField.DefaultValue = "";

            fields.Add(imageField);

            MultiLineTextField multiLineTextField = new MultiLineTextField();

            multiLineTextField.Id = Guid.NewGuid();
            multiLineTextField.Name = "MultiLineTextField";
            multiLineTextField.Label = "MultiLineText field";
            multiLineTextField.PlaceholderText = "MultiLineText field placeholder text";
            multiLineTextField.Description = "MultiLineText field description";
            multiLineTextField.HelpText = "MultiLineText field help text";
            multiLineTextField.Required = true;
            multiLineTextField.Unique = true;
            multiLineTextField.Searchable = true;
            multiLineTextField.Auditable = true;
            multiLineTextField.System = true;
            multiLineTextField.DefaultValue = "";

            multiLineTextField.MaxLength = 500;
            multiLineTextField.VisibleLineNumber = 10;

            fields.Add(multiLineTextField);

            MultiSelectField multiSelectField = new MultiSelectField();

            multiSelectField.Id = Guid.NewGuid();
            multiSelectField.Name = "MultiSelectField";
            multiSelectField.Label = "MultiSelect field";
            multiSelectField.PlaceholderText = "MultiSelect field placeholder text";
            multiSelectField.Description = "MultiSelect field description";
            multiSelectField.HelpText = "MultiSelect field help text";
            multiSelectField.Required = true;
            multiSelectField.Unique = true;
            multiSelectField.Searchable = true;
            multiSelectField.Auditable = true;
            multiSelectField.System = true;
            multiSelectField.DefaultValue = new string[] { "itemKey1", "itemKey4" };

            multiSelectField.Options = new List<MultiSelectFieldOption>();
            multiSelectField.Options.Add(new MultiSelectFieldOption("itemKey1", "itemValue1"));
            multiSelectField.Options.Add(new MultiSelectFieldOption("itemKey2", "itemValue2"));
            multiSelectField.Options.Add(new MultiSelectFieldOption("itemKey3", "itemValue3"));
            multiSelectField.Options.Add(new MultiSelectFieldOption("itemKey4", "itemValue4"));
            multiSelectField.Options.Add(new MultiSelectFieldOption("itemKey5", "itemValue5"));
            multiSelectField.Options.Add(new MultiSelectFieldOption("itemKey6", "itemValue6"));

            fields.Add(multiSelectField);

            NumberField numberField = new NumberField();

            numberField.Id = Guid.NewGuid();
            numberField.Name = "NumberField";
            numberField.Label = "Number field";
            numberField.PlaceholderText = "Number field placeholder text";
            numberField.Description = "Number field description";
            numberField.HelpText = "Number field help text";
            numberField.Required = true;
            numberField.Unique = true;
            numberField.Searchable = true;
            numberField.Auditable = true;
            numberField.System = true;
            numberField.DefaultValue = 0;

            numberField.MinValue = 1;
            numberField.MaxValue = 100;
            numberField.DecimalPlaces = 3;

            fields.Add(numberField);

            PasswordField passwordField = new PasswordField();

            passwordField.Id = Guid.NewGuid();
            passwordField.Name = "PasswordField";
            passwordField.Label = "Password field";
            passwordField.PlaceholderText = "Password field placeholder text";
            passwordField.Description = "Password field description";
            passwordField.HelpText = "Password field help text";
            passwordField.Required = true;
            passwordField.Unique = true;
            passwordField.Searchable = true;
            passwordField.Auditable = true;
            passwordField.System = true;

            passwordField.MaxLength = 1;
            passwordField.MaskType = Api.PasswordFieldMaskTypes.MaskAllCharacters;
            passwordField.MaskCharacter = '*';

            fields.Add(passwordField);

            PercentField percentField = new PercentField();

            percentField.Id = Guid.NewGuid();
            percentField.Name = "PercentField";
            percentField.Label = "Percent field";
            percentField.PlaceholderText = "Percent field";
            percentField.Description = "Percent field description";
            percentField.HelpText = "Percent field help text";
            percentField.Required = true;
            percentField.Unique = true;
            percentField.Searchable = true;
            percentField.Auditable = true;
            percentField.System = true;
            percentField.DefaultValue = 0;

            percentField.MinValue = 1;
            percentField.MaxValue = 100;
            percentField.DecimalPlaces = 3;

            fields.Add(percentField);

            PhoneField phoneField = new PhoneField();

            phoneField.Id = Guid.NewGuid();
            phoneField.Name = "PhoneField";
            phoneField.Label = "Phone field";
            phoneField.PlaceholderText = "Phone field";
            phoneField.Description = "Phone field description";
            phoneField.HelpText = "Phone field help text";
            phoneField.Required = true;
            phoneField.Unique = true;
            phoneField.Searchable = true;
            phoneField.Auditable = true;
            phoneField.System = true;
            phoneField.DefaultValue = "";

            phoneField.Format = "{0000}-{000}-{000}";
            phoneField.MaxLength = 10;

            fields.Add(phoneField);

            GuidField primaryKeyField = new GuidField();

            primaryKeyField.Id = Guid.NewGuid();
            primaryKeyField.Name = "PrimaryKeyField";
            primaryKeyField.Label = "PrimaryKey field";
            primaryKeyField.PlaceholderText = "PrimaryKey field placeholder text";
            primaryKeyField.Description = "PrimaryKey field description";
            primaryKeyField.HelpText = "PrimaryKey field help text";
            primaryKeyField.Required = true;
            primaryKeyField.Unique = true;
            primaryKeyField.Searchable = true;
            primaryKeyField.Auditable = true;
            primaryKeyField.System = true;
            primaryKeyField.DefaultValue = Guid.Empty;

            fields.Add(primaryKeyField);

            SelectField selectField = new SelectField();

            selectField.Id = Guid.NewGuid();
            selectField.Name = "SelectField";
            selectField.Label = "Select field";
            selectField.PlaceholderText = "Select field placeholder text";
            selectField.Description = "Select field description";
            selectField.HelpText = "Select field help text";
            selectField.Required = true;
            selectField.Unique = true;
            selectField.Searchable = true;
            selectField.Auditable = true;
            selectField.System = true;
            selectField.DefaultValue = "itemKey2";

            selectField.Options = new List<SelectFieldOption>();
            selectField.Options.Add(new SelectFieldOption("itemKey1", "itemValue1"));
            selectField.Options.Add(new SelectFieldOption("itemKey2", "itemValue2"));
            selectField.Options.Add(new SelectFieldOption("itemKey3", "itemValue3"));
            selectField.Options.Add(new SelectFieldOption("itemKey4", "itemValue4"));
            selectField.Options.Add(new SelectFieldOption("itemKey5", "itemValue5"));
            selectField.Options.Add(new SelectFieldOption("itemKey6", "itemValue6"));

            fields.Add(selectField);

            TextField textField = new TextField();

            textField.Id = Guid.NewGuid();
            textField.Name = "TextField";
            textField.Label = "Text field";
            textField.PlaceholderText = "Text field placeholder text";
            textField.Description = "Text field description";
            textField.HelpText = "Text field help text";
            textField.Required = true;
            textField.Unique = true;
            textField.Searchable = true;
            textField.Auditable = true;
            textField.System = true;
            textField.DefaultValue = "";

            textField.MaxLength = 200;

            UrlField urlField = new UrlField();

            urlField.Id = Guid.NewGuid();
            urlField.Name = "UrlField";
            urlField.Label = "Url field";
            urlField.PlaceholderText = "Url field placeholder text";
            urlField.Description = "Url field description";
            urlField.HelpText = "Url field help text";
            urlField.Required = true;
            urlField.Unique = true;
            urlField.Searchable = true;
            urlField.Auditable = true;
            urlField.System = true;
            urlField.DefaultValue = "";

            urlField.MaxLength = 200;
            urlField.OpenTargetInNewWindow = true;

            fields.Add(urlField);

            return fields;
        }

        private List<RecordsList> CreateTestViewCollection(Entity entity)
        {
            List<RecordsList> recordsLists = new List<RecordsList>();

            RecordsList firstRecordList = new RecordsList();

            firstRecordList.Id = Guid.NewGuid();
            firstRecordList.Name = "SearchPopupviewname";
            firstRecordList.Label = "Search Popup view label";
            firstRecordList.Type = Api.ViewTypes.SearchPopup;

            firstRecordList.Filters = new List<RecordsListFilter>();

            RecordsListFilter filter = new RecordsListFilter();
            filter.EntityId = entity.Id;
            filter.FieldId = entity.Fields[1].Id.Value;
            filter.Operator = Api.FilterOperatorTypes.Equals;
            filter.Value = "false";

            firstRecordList.Filters.Add(filter);

            firstRecordList.Fields = new List<RecordsListField>();

            RecordsListField field1 = new RecordsListField();
            field1.EntityId = entity.Id;
            field1.Id = entity.Fields[3].Id.Value;
            field1.Position = 1;

            firstRecordList.Fields.Add(field1);

            RecordsListField field2 = new RecordsListField();
            field2.EntityId = entity.Id;
            field2.Id = entity.Fields[10].Id.Value;
            field2.Position = 2;

            firstRecordList.Fields.Add(field2);

            recordsLists.Add(firstRecordList);
            return recordsLists;
        }

        private List<RecordView> CreateTestFormCollection(Entity entity)
        {
            List<RecordView> recordViewList = new List<RecordView>();

            RecordView recordView = new RecordView();

            recordView.Id = Guid.NewGuid();
            recordView.Name = "record_view_name";
            recordView.Label = "Record view label";

            recordView.Fields = new List<RecordViewField>();

            RecordViewField field1 = new RecordViewField();

            field1.Id = entity.Fields[1].Id.Value;
            field1.EntityId = entity.Id;
            field1.Column = Api.FormColumns.Left;
            field1.Position = 1;

            recordView.Fields.Add(field1);

            RecordViewField field2 = new RecordViewField();

            field2.Id = entity.Fields[5].Id.Value;
            field2.EntityId = entity.Id;
            field2.Column = Api.FormColumns.Right;
            field2.Position = 2;

            recordView.Fields.Add(field2);

            recordViewList.Add(recordView);

            return recordViewList;
        }
    }
}