pipeline{
    agent any

    environment{
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_NOLOGO = 'true'
        BUILD_CONFIG = 'Release'
        APP_NAME = 'MyApp'
        PUBLISH_DIR = 'publish'
        IIS_SITE = 'MyApp'
        IIS_PATH = 'C:\\inetpub\\wwwroot\\publish'
        APP_URL = 'http://localhost:8087'
    }

    stages{

        stage('Validate SDK'){
            steps{
                bat '''
                where dotnet
                dotnet --version
                '''
            }
        }

        stage('Checkout Code'){
            steps{
                git branch : 'main',
                url : 'https://github.com/shohail-DeV/My-Dotnet-app.git'
            }
        }

        stage('Restore'){
            steps{
                bat 'dotnet restore'
            }
        }

        stage('Build'){
            steps{
                bat "dotnet build --configuration %BUILD_CONFIG% --no-restore"
            }
        }

        stage('Test'){
            steps{
                bat "dotnet test --configuration %BUILD_CONFIG% --no-build"
            }
        }

        stage('Publish'){
            steps{
                bat '''
                dotnet publish MyApp.csproj ^
                --configuartion Release ^
                --no-build ^
                --output publish
                '''
            }
        }

    }

}