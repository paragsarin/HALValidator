pipeline {
    agent any
    environment {
		DOCKER_REGISTRY='hub.docker.com'
		DOCKER_REGISTRY_PROTOCOL='https'
		DOCKER_REGISTRY_CREDENTIAL= credentials('riskmaster-jenkins-ci')
	        BUILD_IMAGE='paragsarin/halvalidator:1.1'    
	    	DOCKER_REGISTRY_AUTH_USERNAME="%DOCKER_REGISTRY_CREDENTIAL_USR%"
   		DOCKER_REGISTRY_AUTH_PASSWORD="%DOCKER_REGISTRY_CREDENTIAL_PSW%"
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
			    echo "${env.DOCKER_REGISTRY_CREDENTIAL}" 
			    echo "${env.DOCKER_REGISTRY_CREDENTIAL_PSW}" 
			  //bat "docker system prune -f -a"
			  bat "docker login ${env.DOCKER_REGISTRY} --username \"${env.DOCKER_REGISTRY_AUTH_USERNAME}\" --password \"${env.DOCKER_REGISTRY_AUTH_PASSWORD}\"" 
			  bat "docker pull ${env.DOCKER_REGISTRY}/${env.BUILD_IMAGE}"
			
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
