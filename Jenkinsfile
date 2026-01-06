pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_NOLOGO = 'true'
        BUILD_CONFIG = 'Release'
        APP_NAME = 'MyApp'
        PUBLISH_DIR = 'publish'
    }

    stages {

         stage('Validate Tooling') {
            steps {
                bat '''
                where dotnet
                dotnet --version
                '''
            }
        }

        stage('Hello DotNet') {
            steps {
                echo 'Hello MyApp using .NET 8.0!'
            }
        }

        stage('Cloning the repo') {
            steps {
                git branch: 'main', url: 'https://github.com/shohail-DeV/My-Dotnet-app.git'
            }
        }

        stage('Restore Dependencies') {
            steps {
                echo 'Restoring NuGet packages'
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                echo 'Building application'
                bat "dotnet build --configuration %BUILD_CONFIG% --no-restore"
            }
        }

        stage('Test') {
            steps {
                echo 'Running tests (if any)'
                bat "dotnet test --configuration %BUILD_CONFIG% --no-build --verbosity normal"
            }
        }

        stage('Publish') {
            steps {
                echo 'Publishing application'
                bat """
                dotnet publish ^
                  --configuration %BUILD_CONFIG% ^
                  --no-build ^
                  --output %PUBLISH_DIR%
                """
            }
        }

        stage('Archive Artifact') {
            steps {
                echo 'Archiving published output'
                archiveArtifacts artifacts: "${PUBLISH_DIR}/**", fingerprint: true
            }
        }

        // OPTIONAL – enable when you have a server
        /*
        stage('Deploy') {
            steps {
                echo 'Deploying application'
                bat '''
                xcopy %PUBLISH_DIR% C:\\deployments\\MyApp /E /I /Y
                '''
            }
        }
        */
    }

    post {
        success {
            echo 'Pipeline completed successfully'
        }
        failure {
            echo 'Pipeline failed – immediate attention required'
        }
        always {
            cleanWs()
        }
    }
}
