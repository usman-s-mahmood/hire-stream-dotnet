# **HireStream** - Your Dream Job Finder! 🚀  

[![Live Site](https://img.shields.io/badge/Live%20Demo-HireStream-brightgreen)](https://hirestream-h6f7gtd5azara2c9.westindia-01.azurewebsites.net/)  

**HireStream** is a modern **ASP.NET Core MVC** platform designed to streamline job searching and hiring. It provides **job seekers** with an intuitive way to find opportunities and **recruiters** with a powerful tool to manage applicants.  

🔥 **Now running in production on [Microsoft Azure](https://hirestream-h6f7gtd5azara2c9.westindia-01.azurewebsites.net/)** with a **high-performance database hosted on Aiven Console!**  

---

## **🚀 Live Demo**  

🔗 **[Try HireStream Now!](https://hirestream-h6f7gtd5azara2c9.westindia-01.azurewebsites.net/)**  

✅ **Test Admin Credentials:**  
- **Email:** `admin@admin.com`  
- **Password:** `adminuser`  

---

## **📌 Key Features**  

✅ **Secure & Scalable Authentication**  
- **BCrypt-based password hashing**  
- **Forgot password & password reset**  
- **Edit profile & account settings**  
- **Account deletion for privacy control**  

✅ **Role-Based Controllers for Efficient Management**  
- **Talent Controller** – For **Recruiters** to manage job listings  
- **JobSeeker Controller** – For **Applicants** to search and apply for jobs  

✅ **Advanced Job Search & Pagination**  
- **Smart job filtering** by category, location, salary, and job type  
- **Pagination for smooth browsing**  

✅ **Dynamic Admin Panel** 🛠  
- Custom-built **admin dashboard** to manage users, jobs, and applications  

✅ **Optimized Job Posting System**  
- **Categories, salary details, job type, and location fields**  
- **Application tracking system** for recruiters  

✅ **Newsletter Subscription** 📩  
- Users can subscribe to job alerts  

✅ **Production-Ready Deployment**  
- **Hosted on Microsoft Azure** 🌐  
- **Database powered by Aiven Console** for high availability  

✅ **Modern & Responsive UI**  
- Built with **Bootstrap 5** for a seamless experience  

---

## **🛠️ Tech Stack**  

| Technology           | Description                                  |  
|----------------------|----------------------------------------------|  
| **ASP.NET Core MVC** | Backend framework for business logic        |  
| **.NET 8**          | Latest .NET version for high performance     |  
| **Bootstrap 5**     | Frontend UI framework for responsive design  |  
| **MySQL (Aiven DB)** | Scalable, cloud-hosted database on Aiven    |  
| **BCrypt**          | Secure password encryption                   |  
| **Microsoft Azure** | Cloud hosting for reliability & scalability  |  

---

## **📖 How to Run Locally**  

1️⃣ **Clone the repository**  

```sh
git clone https://github.com/usman-s-mahmood/hire-stream-dotnet.git
cd hire-stream-dotnet/HireStreamDotNetProject
```

2️⃣ **Install dependencies**  

Ensure you have **.NET 8 SDK** installed.  

```sh
dotnet restore
```

3️⃣ **Set up the database connection and Email Service**  

Create a `db_secret.json` file in the project root with the following format:  

```json
{
  "Server": "server.hosting.database.dummy.link",
  "Port": "404405",
  "Database": "yoursuperdb",
  "User": "yourusername",
  "Password": "your-secure-password",
  "SslMode": "Required"
}
```

Create a `secrets.json` file in the project root with the following format:  

```json
{
    "Email": "youremail@example.com",
    "Password": "your app password!",
    "Host": "your.smtp.host.com"
}
```



4️⃣ **Run the application**  

```sh
dotnet run
```

5️⃣ Open your browser and go to:  

👉 `http://localhost:5000`  

---

## **👨‍💻 Project Team**  

This project is a collaborative effort by:  

- **Hafiz Abdul Hannan**  
- **Ismail Mehmood**  
- **Usman Shahid**  
- **Zaid Chughtai**  

📌 **Submitted to:** *Prof. Asad Kamal 👉 [LinkedIn](https://www.linkedin.com/in/asad-kamal-44404b101/)*  

---

## **📌 Future Enhancements**  

🚀 **Upcoming features:**  
✅ **AI-powered job recommendations**  
✅ **Resume Builder & Profile Strength Indicator**  
✅ **Real-time chat between recruiters & applicants**  

---

## **📜 License**  

This project is **open-source** and available under the **MIT License**.  

---

🎯 **Start your job-hunting journey today!** Visit 👉 **[HireStream](https://hirestream-h6f7gtd5azara2c9.westindia-01.azurewebsites.net/)** 🚀  

---
