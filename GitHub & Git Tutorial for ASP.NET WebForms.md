## GitHub & Git Tutorial for ASP.NET Web Forms (C#)

A quick guide to get your team up and running with GitHub, Git, and Visual Studio for our ASP.NET Web Forms project.

---

### 🚀 Part 1: Setup GitHub and Git

#### 1. Create a GitHub Account

1. Navigate to [github.com](https://github.com/)
2. Click **Sign up**
3. Enter your email, password, and desired username
4. Verify your email and complete the onboarding steps

#### 2. Install Git

1. Go to [git-scm.com/downloads](https://git-scm.com/downloads)
2. Download the version for your OS (Windows, macOS, or Linux)
3. Run the installer with default settings
4. After installation, verify by right‑clicking on your desktop (look for **Git Bash**)

---

### 📂 Part 2: Clone & Open the Project

#### 3. Clone via GitHub Desktop (Beginner‑Friendly)

1. Download GitHub Desktop from [desktop.github.com](https://desktop.github.com/)
2. Install and launch GitHub Desktop
3. Sign in with your GitHub credentials
4. Go to **File > Clone repository**
5. Select the **URL** tab and paste our repo link:

   ```
   https://github.com/your‑group/project‑name.git
   ```
6. Choose a local folder (e.g., `Documents\Visual Studio 2022\Projects`)
7. Click **Clone**

#### 4. Open in Visual Studio

1. Launch **Visual Studio**
2. Choose **Open > Project/Solution**
3. Browse to the folder you cloned into
4. Open the `.sln` file

---

### 💻 Part 3: Make Changes & Push

#### 5. Commit & Push Changes

1. Make your code edits in Visual Studio
2. Switch to **GitHub Desktop**
3. Review your modified files under the **Changes** tab
4. Enter a concise commit message (e.g., `Added login backend`)
5. Click **Commit to main**
6. Click **Push origin** to upload your commit to GitHub

---

### 🔄 Part 4: Stay in Sync

#### 6. Pull Latest Updates

Before you start any work each day:

1. Open GitHub Desktop
2. Click **Fetch origin**
3. Click **Pull origin**
4. Resolve any merge conflicts if prompted

---

### 🛠️ Optional: Using Git Bash (Advanced)

```bash
# Configure your identity (only once)
git config --global user.name "Your Name"
git config --global user.email "you@example.com"

# Clone the repository
git clone https://github.com/your‑group/project‑name.git
cd project‑name

# After editing files
git add .
git commit -m "Your commit message"
git push origin main
```

---

### 💡 Helpful Tips

* **Always pull** before you push to minimize merge conflicts.
* **Commit early and often**—small, focused commits make code review easier.
* **Communicate** with teammates about who’s working on which feature or file.

