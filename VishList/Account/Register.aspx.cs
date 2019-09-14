﻿using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using Logic;
using VishList;

public partial class Account_Register : Page
{
    protected void CreateUser_Click(object sender, EventArgs e)
    {
        var manager = new UserManager();
        var user = new ApplicationUser() { UserName = UserName.Text };
        IdentityResult result = manager.Create(user, Password.Text);
        if (result.Succeeded)
        {
            IdentityHelper.SignIn(manager, user, isPersistent: false);
            using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
            {
	            String cartId = usersShoppingCart.GetCartId();
	            usersShoppingCart.MigrateCart(cartId, user.Id);
            }
			IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
        }
        else
        {
            ErrorMessage.Text = result.Errors.FirstOrDefault();
        }
    }
}