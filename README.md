# TranslateApp

TranslateApp is a simple web application built with ASP.NET Core MVC that allows users to translate text into various languages using the OpenAI API.

## Description

This application provides a user-friendly interface to input text, select a target language from a predefined list, and receive a translation powered by OpenAI's GPT models. It serves as a basic demonstration of integrating an external API for language translation within an ASP.NET Core project.

## Features

* Text input for translation.
* Selection of target language from a dropdown list.
* Integration with OpenAI API for translation.
* Displays the translation result.

## Technologies Used

* **Backend:** C# with ASP.NET Core MVC (.NET 9)
* **Frontend:** HTML, CSS, (minimal JavaScript if any)
* **API Integration:** OpenAI API (using `HttpClient`)
* **JSON Handling:** `Newtonsoft.Json`(for serializing/deserializing API requests/responses)

## Prerequisites

* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* A valid OpenAI API Key.
* Git for cloning the repository.

## Setup and Local Installation

1.  **Clone the Repository:**
    ```bash
    git clone [https://github.com/DimSpyropoulos/TranslateApp.git](https://github.com/DimSpyropoulos/TranslateApp.git)
    cd TranslateApp
    ```

2.  **Configure OpenAI API Key:**
    It is crucial to keep your API key secure and not commit it to the repository. The recommended way to handle secrets during local development is using the .NET User Secrets Manager.

    * Navigate to the project directory (where the `.csproj` file is located, e.g., `TranslateApp/TranslateApp/`):
        ```bash
        cd TranslateApp
        ```
    * Initialize user secrets for the project:
        ```bash
        dotnet user-secrets init
        ```
    * Set your OpenAI API key:
        ```bash
        dotnet user-secrets set "OpenAI:ApiKey" "YOUR_ACTUAL_OPENAI_API_KEY"
        ```
        Replace `"YOUR_ACTUAL_OPENAI_API_KEY"` with your real OpenAI API key.

    Alternatively, for development purposes only, you could use `appsettings.Development.json`. Create this file in the `TranslateApp/TranslateApp/` directory if it doesn't exist:
    ```json
    // In TranslateApp/appsettings.Development.json
    {
      "OpenAI": {
        "ApiKey": "YOUR_ACTUAL_OPENAI_API_KEY"
      },
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      }
    }
    ```
    **Warning:** Ensure `appsettings.Development.json` (or any file containing real keys) is listed in your `.gitignore` file to prevent accidental commits. Your current `.gitignore` seems to exclude `bin/` and `obj/`, which is good. The source `appsettings.json` should ideally only contain placeholders.

3.  **Build the Project:**
    Navigate to the solution directory (where `TranslateApp.sln` is located) or the project directory and run:
    ```bash
    dotnet build
    ```

4.  **Run the Application:**
    Navigate to the project directory (`TranslateApp/TranslateApp/`) and run:
    ```bash
    dotnet run
    ```
    The application will typically be available at `https://localhost:XXXX` or `http://localhost:YYYY` (check the console output for the exact URLs).

## Usage

1.  Open the application in your web browser.
2.  Enter the text you wish to translate in the provided textarea.
3.  Select the target language from the dropdown list.
4.  Click the "Translate" button.
5.  The translated text should be displayed (Note: The `GetGPTResponse` action in `HomeController` may need to be updated to properly pass the translated string from the API response to the view for display).

## Important Notes

* **API Key Security:** **Never** commit your actual OpenAI API key to your Git repository. Use User Secrets, environment variables, or a secure configuration management service for production.
* **Language List:** The list of supported languages for translation is currently hardcoded in `HomeController.cs`. This could be made dynamic or expanded.
* **Displaying Results:** The `GetGPTResponse` action method in `HomeController` needs to parse the JSON response from the OpenAI API and pass the actual translated text to a view model or `ViewBag` property so it can be rendered on the page for the user. Currently, it returns `View()` without explicitly passing the result for display.
* **Error Handling:** Implement robust error handling for API calls (e.g., network issues, invalid API key, API rate limits, quota issues).

---
