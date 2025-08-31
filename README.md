# 🛒 E-Commerce Platform (Single Vendor)

## Overview
This is a **single-vendor e-commerce platform** where one **Admin (store owner)** manages the shop and multiple **Buyers** interact with it.  
The system covers all essential e-commerce operations with a secure and scalable backend.

---

## Core Modules

### 1. Authentication & Users
- Use **ASP.NET Identity + JWT** for secure authentication.  
- **Admin** = store owner (only seller).  
- **Buyers** = customers who register/login to shop.  

### 2. Products & Categories
- Admin can manage products (add, update, delete).  
- Products belong to categories (support hierarchy).  
- Each product can have multiple images.  
- Buyers can browse and filter products.  

### 3. Cart
- Buyers can add/remove/update product quantities.  
- Cart is tied to Buyer account.  
- On successful checkout, cart is cleared.  

### 4. Orders
- Buyers create orders from their cart.  
- Orders store a snapshot of product info (name, price, qty).  
- Admin can view/manage all orders and update status:  
  - Pending → Shipped → Delivered → Canceled.  

### 5. Wishlist
- Buyers can save products for later purchase.  
- Items can be moved from wishlist to cart.  

### 6. Discounts
- Admin can create/manage discount codes:  
  - Percentage or fixed amount.  
  - Apply to specific products or categories.  
- Buyers apply valid codes during checkout.  

### 7. Payments
- Admin defines supported payment methods (Mada, Apple Pay, PayPal, COD).  
- Buyers select a payment method during checkout.  
- Transactions track status: Pending, Paid, Failed, Refunded, Canceled.  
- Buyers can optionally save payment methods for reuse.  

---

## Tech Stack
- **Backend**: ASP.NET Core Web API
- **FrontEnd**: React framework
- **Authentication**: ASP.NET Identity + JWT  
- **Payment Gateways**: Mada, Apple Pay, PayPal, COD  
- **Deployment**: MonsterAPi + Vercel

---

## Git Branching Methodology

### Main Branches
- **master** → Stable branch (live).  
- **dev** → Active development branch.  

### Specialized Branches
- **features/** → New features (e.g., wishlist).  
- **bug-fix/** → Fixing bugs.  
- **refactor/** → Code improvements.  
- **hot-fix/** → Urgent fixes from production.  
- **hot-features/** → Urgent production features.  

### Branch Naming
Pattern: `[type]/[module]/[description]`  

Examples:  
- `features/cart/add-quantity-update`  
- `bug-fix/orders/fix-shipping-calculation`  
- `hot-fix/payments/resolve-paypal-error`  

Modules for subfolders:  
- **auth**, **products**, **orders**, **cart**, **wishlist**, **discounts**, **payments**, **admin**  

---

##  Workflow
- Work branches → from `dev`.  
- Merge back into `dev` after review.  
- Hotfixes → from `master`, then merged into both `master` and `dev`.  

