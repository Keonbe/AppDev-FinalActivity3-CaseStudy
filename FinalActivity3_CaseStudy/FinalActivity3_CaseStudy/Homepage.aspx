<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterpage.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="FinalActivity3_CaseStudy.Homepage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .hero-section {
            background-color: #f0f6ff;
            border-radius: 8px;
            padding: 2rem;
            margin-bottom: 2rem;
            text-align: center;
        }
        
        .hero-title {
            font-size: 2.5rem;
            color: #4a6fa5;
            margin-bottom: 1rem;
        }
        
        .hero-description {
            font-size: 1.1rem;
            color: #555;
            margin-bottom: 2rem;
            max-width: 800px;
            margin-left: auto;
            margin-right: auto;
        }
        
        .features-section {
            display: flex;
            flex-wrap: wrap;
            gap: 2rem;
            justify-content: center;
            margin-bottom: 2rem;
        }
        
        .feature-card {
            background-color: white;
            border-radius: 8px;
            padding: 1.5rem;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            width: 300px;
            text-align: center;
        }
        
        .feature-icon {
            font-size: 2rem;
            color: #4a6fa5;
            margin-bottom: 1rem;
        }
        
        .feature-title {
            font-size: 1.2rem;
            font-weight: bold;
            margin-bottom: 0.5rem;
            color: #333;
        }
        
        .feature-description {
            color: #666;
        }
        
        .auth-section {
            background-color: #f0f6ff;
            border-radius: 8px;
            padding: 2rem;
            text-align: center;
            max-width: 600px;
            margin: 0 auto;
        }
        
        .auth-title {
            font-size: 1.5rem;
            color: #4a6fa5;
            margin-bottom: 1rem;
        }
        
        .auth-description {
            color: #555;
            margin-bottom: 1.5rem;
        }
        
        .auth-buttons {
            display: flex;
            gap: 1rem;
            justify-content: center;
        }
        
        .btn {
            padding: 0.75rem 1.5rem;
            font-size: 1rem;
            border-radius: 4px;
            cursor: pointer;
            font-weight: bold;
            transition: all 0.2s ease;
            text-decoration: none;
            border: none;
        }
        
        .btn-primary {
            background-color: #4a6fa5;
            color: white;
        }
        
        .btn-primary:hover {
            background-color: #3a5982;
        }
        
        .btn-secondary {
            background-color: #e0e0e0;
            color: #333;
        }
        
        .btn-secondary:hover {
            background-color: #c4c4c4;
        }
        
        .landing-content {
            max-width: 1000px;
            margin: 0 auto;
            padding: 2rem 0;
        }
        
        /* Responsive adjustments */
        @media (max-width: 768px) {
            .features-section {
                flex-direction: column;
                align-items: center;
            }
            
            .feature-card {
                width: 100%;
                max-width: 300px;
            }
            
            .auth-buttons {
                flex-direction: column;
            }
            
            .hero-title {
                font-size: 2rem;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="landing-content">
        <section class="hero-section">
            <h1 class="hero-title">Welcome to Our Sales & Inventory System</h1>
            <p class="hero-description">
                Streamline your business operations with our comprehensive sales and inventory management solution.
                Track products, manage orders, and gain valuable insights all in one place.
            </p>
        </section>
        
        <section class="features-section">
            <div class="feature-card">
                <div class="feature-icon">📦</div>
                <h3 class="feature-title">Inventory Management</h3>
                <p class="feature-description">
                    Track stock levels, set reorder points, and manage product information with ease.
                </p>
            </div>
            
            <div class="feature-card">
                <div class="feature-icon">💰</div>
                <h3 class="feature-title">Sales Tracking</h3>
                <p class="feature-description">
                    Monitor your sales performance, track customer orders, and manage transactions.
                </p>
            </div>
            
            <div class="feature-card">
                <div class="feature-icon">📊</div>
                <h3 class="feature-title">Analytics & Reports</h3>
                <p class="feature-description">
                    Generate insightful reports and analyze your business performance with powerful tools.
                </p>
            </div>
        </section>
        
        <section class="auth-section">
            <h2 class="auth-title">Get Started Today</h2>
            <p class="auth-description">
                Login to your account or register for a new one to start managing your inventory and sales.
            </p>
            
            <div class="auth-buttons">
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-secondary" OnClick="btnRegister_Click" />
            </div>
        </section>
    </div>
</asp:Content>