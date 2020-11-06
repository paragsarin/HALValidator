pipeline {
    agent any
    environment {
		DOCKER_REGISTRY='hub.docker.com'
		DOCKER_REGISTRY_PROTOCOL='https'
		DOCKER_REGISTRY_CREDENTIAL= credentials('riskmaster-jenkins-ci')
	        BUILD_IMAGE='mcr.microsoft.com/dotnet/core/sdk:3.1-buster'  
            LOCAL_BUILD_IMAGE='builder'      
	    	DOCKER_REGISTRY_AUTH_USERNAME="%DOCKER_REGISTRY_CREDENTIAL_USR%"
   		DOCKER_REGISTRY_AUTH_PASSWORD="%DOCKER_REGISTRY_CREDENTIAL_PSW%"
           GITHUB_DXC_CREDENTIALS=credentials('githubauth')
           GITHUB_DXC_CREDENTIALS_USERNAME="%DOCKER_REGISTRY_CREDENTIAL_USR%"
           GITHUB_DXC_CREDENTIALS_PWD="%DOCKER_REGISTRY_CREDENTIAL_PSW%"
	}

    stages {
        stage('Build') {
            steps {
             script {
               echo 'Build...'
      
              }
              
            }
        }
	    
	    stage ("Pull base images") {	
		  steps {
			   script {
			   env.WORKDIR = "${env.WORKSPACE}"
			     echo "${env.WORKDIR}"
			    echo "${env.DOCKER_REGISTRY_CREDENTIAL}"
			    echo "${env.DOCKER_REGISTRY_CREDENTIAL_PSW}"
			  //bat "docker system prune -f -a"
			  bat "docker login --username \"${env.DOCKER_REGISTRY_AUTH_USERNAME}\" --password \"${env.DOCKER_REGISTRY_AUTH_PASSWORD}\"" 
			  bat "docker pull ${env.BUILD_IMAGE}"
                bat "docker tag ${env.BUILD_IMAGE} ${env.LOCAL_BUILD_IMAGE}"
                bat "docker run --name buildcontainer_${env.BUILD_NUMBER} -e rusername=\"${env.GITHUB_DXC_CREDENTIALS_USERNAME}\" -e rpassword=\"${env.GITHUB_DXC_CREDENTIALS_PWD}\" -e AUTH_USERNAME=\"${env.DOCKER_REGISTRY_AUTH_USERNAME}\" -e AUTH_PASSWORD=\"${env.DOCKER_REGISTRY_AUTH_PASSWORD}\" -e BUILD_NUMBER=${env.BUILD_NUMBER} -w src -d -it ${env.LOCAL_BUILD_IMAGE}" 
          		bat "docker exec buildcontainer_${env.BUILD_NUMBER} powershell git clone --single-branch -b ${env.CHECKOUT_BRANCH} --depth=1 https://github.com/paragsarin/HALValidator.git"
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
  post 
			{	
			   always 
				{
					echo 'Cleanup windows items'
					bat "docker logout hub.docker.com"
				}
			}	
}
