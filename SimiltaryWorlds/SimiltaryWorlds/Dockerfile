# # # # # שלב 1: בנייה (Build)
# # # # FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# # # # WORKDIR /src

# # # # # העתק את קובץ הפרויקט ובצע Restore
# # # # COPY SimiltaryWorlds.csproj ./
# # # # RUN dotnet restore

# # # # # העתק את כל שאר הקבצים
# # # # COPY . ./

# # # # # בנה ופרסם את הפרויקט
# # # # RUN dotnet publish -c Release -o /app/publish

# # # # # שלב 2: הרצה (Runtime)
# # # # FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# # # # WORKDIR /app

# # # # # העתק את הקבצים המפורסמים מהשלב הראשון
# # # # COPY --from=build /app/publish .

# # # # # פקודת הרצה
# # # # ENTRYPOINT ["dotnet", "SimiltaryWorlds.dll"]


# # # # # שלב 1: בנייה (Build)
# # # # FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# # # # WORKDIR /src

# # # # # העתק את קובץ הפרויקט ובצע Restore
# # # # COPY SimiltaryWorlds.csproj ./
# # # # RUN dotnet restore

# # # # # העתק את שאר הקבצים
# # # # COPY . ./

# # # # # 💡 שנה ל־Debug במקום Release כדי לאפשר דיבוג
# # # # RUN dotnet build -c Debug -o /app/build
# # # # RUN dotnet publish -c Debug -o /app/publish

# # # # # שלב 2: הרצה (Runtime)
# # # # FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# # # # WORKDIR /app

# # # # # 📢 לא חובה, אבל שימושי לפתיחת פורטים לדיבוג אם תעבוד עם attach:
# # # # EXPOSE 5000
# # # # EXPOSE 5001

# # # # # העתק את הפלט המפורסם
# # # # COPY --from=build /app/publish .

# # # # # נקודת כניסה
# # # # ENTRYPOINT ["dotnet", "SimiltaryWorlds.dll"]



# # # # שלב 1: בנייה (Build)
# # # FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# # # WORKDIR /src

# # # # העתק את קובץ הפרויקט ובצע Restore
# # # COPY SimiltaryWorlds.csproj ./
# # # RUN dotnet restore

# # # # העתק את שאר הקבצים
# # # COPY . ./

# # # # 💡 שנה ל־Debug במקום Release כדי לאפשר דיבוג
# # # RUN dotnet build -c Debug -o /app/build
# # # RUN dotnet publish -c Debug -o /app/publish

# # # # שלב 2: הרצה (Runtime)
# # # FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# # # WORKDIR /app

# # # # 📢 לא חובה, אבל שימושי לפתיחת פורטים לדיבוג אם תעבוד עם attach:
# # # EXPOSE 5000
# # # EXPOSE 5001

# # # # העתק את הפלט המפורסם
# # # COPY --from=build /app/publish .

# # # # נקודת כניסה
# # # ENTRYPOINT ["dotnet", "SimiltaryWorlds.dll"]


# # # שלב 1: Build
# # FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# # WORKDIR /src

# # # העתקת קובץ הפרויקט והרצת restore
# # COPY SimiltaryWorlds.csproj ./
# # RUN dotnet restore

# # # העתקת שאר הקבצים
# # COPY . ./

# # # קומפילציה בתצורת DEBUG
# # RUN dotnet build -c Debug -o /app/build
# # RUN dotnet publish -c Debug -o /app/publish

# # # שלב 2: Runtime עם תמיכה בדיבוג
# # FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# # WORKDIR /app

# # # 📌 התקנת ה־vsdbg (נחוץ לדיבוג מתוך VS Code)
# # RUN apt-get update \
# #   && apt-get install -y unzip curl \
# #   && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

# # # 📢 פתיחת פורטים לדיבוג (אם תשתמש ב־remote attach)
# # EXPOSE 5000
# # EXPOSE 5001

# # # העתקת הקבצים מהשלב הראשון
# # COPY --from=build /app/publish .

# # # הרצת האפליקציה
# # ENTRYPOINT ["dotnet", "SimiltaryWorlds.dll"]


# # שלב 1: בנייה (Build)
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /src

# # העתק את קובץ הפרויקט ובצע Restore
# COPY SimiltaryWorlds.csproj ./
# RUN dotnet restore

# # העתק את כל שאר הקבצים
# COPY . ./

# # פרסם את הפרויקט בתצורת Release
# RUN dotnet publish -c Release -o /app/publish

# # שלב 2: הרצה (Runtime)
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# WORKDIR /app

# # פתח פורטים רגילים (ניתן לשנות לפי הצורך)
# EXPOSE 5000
# EXPOSE 5001

# # העתק את הפלט המפורסם מהשלב הראשון
# COPY --from=build /app/publish .

# # הפעל את האפליקציה
# ENTRYPOINT ["dotnet", "SimiltaryWorlds.dll"]


# שלב 1: Build בקונפיגורציית DEBUG
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# העתקת קובץ הפרויקט והרצת restore
COPY SimiltaryWorlds.csproj ./
RUN dotnet restore

# העתקת שאר הקבצים
COPY . ./

# קומפילציה ו־publish בתצורת DEBUG
RUN dotnet build -c Debug -o /app/build
RUN dotnet publish -c Debug -o /app/publish

# שלב 2: Runtime עם תמיכה בדיבוג
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# 📌 התקנת ה־Visual Studio Debugger
RUN apt-get update \
    && apt-get install -y unzip curl \
    && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

# פתיחת פורטים לדיבוג
EXPOSE 5000
EXPOSE 5001

# העתקת הפלט מהשלב הקודם
COPY --from=build /app/publish .

# הרצת האפליקציה
ENTRYPOINT ["dotnet", "SimiltaryWorlds.dll"]
