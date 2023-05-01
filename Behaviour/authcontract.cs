using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using OnBusyness.Model;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using OnBusyness.Services.EmailService;
using OnBusyness.Services.EmailService.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text;

namespace OnBusyness.Behaviour {
    public class CompaignContract {
        public int flag { get; set; } = 0;
        public string message { get; set; } = string.Empty;

        public async Task<object> sendMailToNonLoyal(IEmailService emailService) {
            using( var db = new bitbyteContext() ) {
                var data = db.TblContacts.Where(x => !x.IsDeleted && !String.IsNullOrEmpty(x.ContactEmail)).Select(x => new {
                    x.ContactEmail,
                    x.ContactName,
                    products = x.TblContactProductInterests.Where(y => !y.TblOurProductProduct.IsDeleted).Select(y => new {
                        y.TblOurProductProduct.ProductId,
                        y.TblOurProductProduct.ProductName,
                        productBullets = y.TblOurProductProduct.TblProductBullets.Where(z => !z.IsDeleted).Select(z => new {
                            z.BulletName,
                            z.BulletDescription
                        }).ToList()
                    }).ToList()
                }).ToList();
                var allProducts = db.TblOurProducts.Select(x => new { x.ProductName, x.ProductId }).ToList();

                string mainMailBody = File.ReadAllText("./compressed.html");
                string prodTemplate = File.ReadAllText("./compressed_newProd.html");
                string followUpTemplate = File.ReadAllText("./compressed_followup.html");

                string successString = "";
                data.ForEach(mailData => {
                    string currfollowUp = new StringBuilder(followUpTemplate).Replace("%name", mailData.ContactName).ToString();
                    string list1 = "";
                    mailData.products.ForEach(product => {
                        list1 += new StringBuilder(prodTemplate).Replace("%img", product.ProductName).ToString();
                    });
                    string list2 = "";
                    var ids = mailData.products.Select(x => x.ProductId).ToHashSet();
                    allProducts.Where(x => !ids.Contains(x.ProductId)).ToList().ForEach(product => {
                        list2 += new StringBuilder(prodTemplate).ToString().Replace("%img", product.ProductName);
                    });
                    string currMail = new StringBuilder(mainMailBody).Replace("%followUp", currfollowUp).Replace("%list1", list1).Replace("%list2", list2).ToString();

                    EmailDto emailDto = new EmailDto { To = mailData.ContactEmail, Subject = "Check out the Products offered by bitbyte", Body = currMail };
                    emailService.SendEmail(emailDto).ContinueWith(isSuccess => {
                        if( isSuccess.IsCompleted ) {
                            successString += mailData.ContactName + "---";
                        }
                    });

                });
                flag = 1;
                message = "Success";

                object[] response = { flag, message, successString };
                return response;
            }

        }

        public object addFestival(TblFestival tblFestival) {
            using( var db = new bitbyteContext() ) {
                TblFestival check = db.TblFestivals.Where(x => x.FestivalId == tblFestival.FestivalId).FirstOrDefault();
                if( check == null ) {
                    db.TblFestivals.Add(tblFestival);
                    flag = db.SaveChanges();
                    if( flag == 1 )
                        message = "Successfully Added.";
                    else
                        message = "Not added Successfully.";
                }
                else {
                    check.FestivalDate = tblFestival.FestivalDate;
                    check.FestivalName = tblFestival.FestivalName;
                    db.SaveChanges();
                    flag = 1;
                    message = "Successfully updated";
                }
            }
            object[] response = { flag, message };
            return response;
        }
        public object addProduct(TblOurProduct tblOurProduct) {
            using( var db = new bitbyteContext() ) {
                db.TblOurProducts.Add(tblOurProduct);
                flag = db.SaveChanges();
                if( flag == 1 )
                    message = "Successfully Added.";
                else
                    message = "Not added Successfully.";
            }
            object[] response = { flag, message };
            return response;
        }
        public object addContact(TblContact tblContact) {
            using( var db = new bitbyteContext() ) {
                TblContact check = db.TblContacts.Where(x => x.ContactId == tblContact.ContactId).FirstOrDefault();
                if( check == null ) {
                    db.TblContacts.Add(tblContact);
                    flag = db.SaveChanges();
                    if( flag == 1 )
                        message = "Successfully Added.";
                    else
                        message = "Not added Successfully.";
                }
                else {
                    check.ContactName = tblContact.ContactName;
                    check.ContactNumber = tblContact.ContactNumber;
                    check.ContactEmail = tblContact.ContactEmail;
                    check.IsDeleted = tblContact.IsDeleted;
                    db.SaveChanges();
                    flag = 1;
                    message = "Successfully updated";
                }

            }
            object[] response = { flag, message };
            return response;
        }
        public object addLoyalContact(TblLoyalContact tblLoyalContact) {
            using( var db = new bitbyteContext() ) {
                db.TblLoyalContacts.Add(tblLoyalContact);
                flag = db.SaveChanges();
                if( flag == 1 )
                    message = "Successfully Added.";
                else
                    message = "Not added Successfully.";
            }
            object[] response = { flag, message };
            return response;
        }


        public class BindingDTO {
            public int contactID { get; set; }
            public List<int> productIDs { get; set; }
        }
        public object addContactProductIntrest(BindingDTO bindingDTO) {
            using( var db = new bitbyteContext() ) {
                List<TblContactProductInterest> tblContactProductInterests = new List<TblContactProductInterest>();
                bindingDTO.productIDs.ForEach(productID => {
                    tblContactProductInterests.Add(new TblContactProductInterest {
                        TblContactsContactId = bindingDTO.contactID,
                        TblOurProductProductId = productID
                    });
                });
                db.TblContactProductInterests.AddRange(tblContactProductInterests);
                flag = db.SaveChanges();
                if( flag == tblContactProductInterests.Count ) {
                    flag = 1;
                }
                else {
                    flag = 0;
                }
                if( flag == 1 )
                    message = "Successfully Added.";
                else
                    message = "Not added Successfully.";
            }
            object[] response = { flag, message };
            return response;
        }
        public object addLoyalContactProductIntrest(BindingDTO bindingDTO) {
            using( var db = new bitbyteContext() ) {
                List<TblLoyalContactProductIntrest> tblLoyalContactProductInterests = new List<TblLoyalContactProductIntrest>();
                bindingDTO.productIDs.ForEach(productID => {
                    tblLoyalContactProductInterests.Add(new TblLoyalContactProductIntrest {
                        TblLoyalContactLoyalContactId = bindingDTO.contactID,
                        TblOurProductProductId = productID
                    });
                });
                db.TblLoyalContactProductIntrests.AddRange(tblLoyalContactProductInterests);
                flag = db.SaveChanges();
                if( flag == tblLoyalContactProductInterests.Count ) {
                    flag = 1;
                }
                else {
                    flag = 0;
                }
                if( flag == 1 )
                    message = "Successfully Added.";
                else
                    message = "Not added Successfully.";
            }
            object[] response = { flag, message };
            return response;
        }




    }
}