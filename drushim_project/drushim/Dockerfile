

# # # # FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# # # # WORKDIR /app

# # # # COPY drushim.csproj ./
# # # # RUN dotnet restore

# # # # COPY . ./
# # # # RUN dotnet publish -c Release -o /app/publish

# # # # FROM mcr.microsoft.com/dotnet/aspnet:8.0
# # # # WORKDIR /app
# # # # COPY --from=build /app/publish .
# # # # ENTRYPOINT ["dotnet", "drushim.dll"]



# # # # שלב 1: Build
# # # # FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# # # # WORKDIR /src

# # # # # העתקת קובץ csproj והרצת restore
# # # # COPY drushim.csproj ./
# # # # RUN dotnet restore

# # # # # העתקת שאר הקבצים והרצת build בתצורת DEBUG
# # # # COPY . ./
# # # # RUN dotnet build -c Debug -o /app/build
# # # # RUN dotnet publish -c Debug -o /app/publish

# # # # # שלב 2: Runtime
# # # # FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# # # # WORKDIR /app

# # # # # פתח פורטים לדיבוג (לא חובה אבל מומלץ)
# # # # EXPOSE 5000
# # # # EXPOSE 5001

# # # # # העתק את הקוד מהשלב הקודם
# # # # COPY --from=build /app/publish .

# # # # # הפעל את האפליקציה
# # # # ENTRYPOINT ["dotnet", "drushim.dll"]


# # # # שלב 1: Build
# # # FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# # # WORKDIR /src

# # # # העתקת קובץ csproj והרצת restore
# # # COPY drushim.csproj ./
# # # RUN dotnet restore

# # # # העתקת שאר הקבצים והרצת build בתצורת DEBUG
# # # COPY . ./
# # # RUN dotnet build -c Debug -o /app/build
# # # RUN dotnet publish -c Debug -o /app/publish

# # # # שלב 2: Runtime
# # # FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# # # WORKDIR /app

# # # # פתח פורטים לדיבוג (לא חובה אבל מומלץ)
# # # EXPOSE 5000
# # # EXPOSE 5001

# # # # העתק את הקוד מהשלב הקודם
# # # COPY --from=build /app/publish .

# # # # הפעל את האפליקציה
# # # ENTRYPOINT ["dotnet", "drushim.dll"]


# # # שלב 1: Build
# # FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# # WORKDIR /src

# # COPY drushim.csproj ./
# # RUN dotnet restore

# # COPY . ./
# # RUN dotnet build -c Debug -o /app/build
# # RUN dotnet publish -c Debug -o /app/publish

# # # שלב 2: Runtime עם vsdbg
# # FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# # WORKDIR /app

# # # התקנת Visual Studio Debugger (vsdbg)
# # RUN apt-get update \
# #   && apt-get install -y unzip curl \
# #   && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

# # # פורטים לדיבוג
# # EXPOSE 5000
# # EXPOSE 5001

# # COPY --from=build /app/publish .

# # ENTRYPOINT ["dotnet", "drushim.dll"]



# # שלב 1: בנייה
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /src

# # העתקת קובץ הפרויקט והרצת restore
# COPY drushim.csproj ./
# RUN dotnet restore

# # העתקת שאר הקבצים והרצת publish
# COPY . ./
# RUN dotnet publish -c Release -o /app/publish

# # שלב 2: הרצה
# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
# WORKDIR /app

# # פורטים לשימוש חיצוני (ניתן להתאים לפי הצורך)
# EXPOSE 5000
# EXPOSE 5001

# # העתקת קבצים מהשלב הראשון
# COPY --from=build /app/publish .

# # הפעלת האפליקציה
# ENTRYPOINT ["dotnet", "drushim.dll"]

# שלב 1: בנייה בקונפיגורציית DEBUG
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# העתקת קובץ הפרויקט והרצת restore
COPY drushim.csproj ./
RUN dotnet restore

# העתקת כל שאר הקבצים והרצת publish עם DEBUG
COPY . ./
RUN dotnet publish -c Debug -o /app/publish

# שלב 2: Runtime כולל התקנת Visual Studio Debugger (vsdbg)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# התקנת כלים לדיבוג
RUN apt-get update \
    && apt-get install -y unzip curl \
    && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

# פתיחת פורטים לדיבוג
EXPOSE 5000
EXPOSE 5001
EXPOSE 80

# העתקת הקבצים שפורסמו מהשלב הקודם
COPY --from=build /app/publish .

# נקודת ההרצה של האפליקציה
ENTRYPOINT ["dotnet", "drushim.dll"]

