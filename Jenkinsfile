pipeline {
    agent any

    environment {
        DOTNET_CLI_TELEMETRY_OPTOUT = '1'
        DOTNET_NOLOGO = 'true'
        BUILD_CONFIG = 'Release'
        APP_NAME = 'MyApp'
        PUBLISH_DIR = 'publish'
        IIS_SITE = 'MyApp'
        IIS_PATH = 'C:\\inetpub\\wwwroot\\publish'
        APP_URL = 'http://localhost:8087'
    }

    stages {

        stage('Validate SDK') {
            steps {
                bat '''
                where dotnet
                dotnet --version
                '''
            }
        }

        stage('Checkout Source') {
            steps {
                git branch: 'main',
                    url: 'https://github.com/shohail-DeV/My-Dotnet-app.git'
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat "dotnet build --configuration %BUILD_CONFIG% --no-restore"
            }
        }

        stage('Test') {
            steps {
                bat "dotnet test --configuration %BUILD_CONFIG% --no-build"
            }
        }

        stage('Publish') {
            steps {
                bat "dotnet publish --configuration %BUILD_CONFIG% --no-build --output %PUBLISH_DIR%"
            }
        }

        /* ======================
           CD STARTS HERE
           ====================== */

        stage('Stop IIS Site') {
            steps {
                bat '''
                %windir%\\system32\\inetsrv\\appcmd stop site "%IIS_SITE%"
                '''
            }
        }

        stage('Deploy to IIS') {
            steps {
                bat '''
                if exist "%IIS_PATH%" rmdir /s /q "%IIS_PATH%"
                mkdir "%IIS_PATH%"
                xcopy "%PUBLISH_DIR%\\*" "%IIS_PATH%\\" /E /Y /I
                '''
            }
        }

        stage('Start IIS Site') {
            steps {
                bat '''
                %windir%\\system32\\inetsrv\\appcmd start site "%IIS_SITE%"
                '''
            }
        }

        stage('Health Check') {
            steps {
                bat '''
                powershell -Command ^
                "try { ^
                    $r = Invoke-WebRequest -Uri %APP_URL% -UseBasicParsing -TimeoutSec 10; ^
                    if ($r.StatusCode -ne 200) { exit 1 } ^
                } catch { exit 1 }"
                '''
            }
        }

        stage('Archive Artifact') {
            steps {
                archiveArtifacts artifacts: "${PUBLISH_DIR}/**", fingerprint: true
            }
        }
    }

    post {
        success {
            echo 'CI/CD pipeline executed successfully'
        }
        failure {
            echo 'Pipeline failed – rollback or investigation required'
        }
    }
}
