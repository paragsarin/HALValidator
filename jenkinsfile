pipeline {
    agent any
    environment {
		DOCKER_REGISTRY='hub.docker.com'
		DOCKER_REGISTRY_PROTOCOL='https'
		DOCKER_REGISTRY_CREDENTIAL='riskmaster-jenkins-ci'
	    RM_BUILD_IMAGE='mcr.microsoft.com/windows/nanoserver:10.0.14393.953'      
	}

    stages {
        stage('Build') {
            steps {
             script {
               echo 'Build...'
      
              }
              
            }
        }
        stage('Test') {
            steps {
            
script {
                echo 'Testing.. Parag'
                def msg = powershell(returnStdout: true, script: 'ipconfig')
                echo msg
}
            }
        }
        stage('Deploy') {
            steps {
                echo 'Deploying....Parag'
            }
        }
    }
}
