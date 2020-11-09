pipeline {
    agent any
    environment {
		DOCKER_REGISTRY='hub.docker.com'
		DOCKER_REGISTRY_PROTOCOL='https'
		DOCKER_REGISTRY_CREDENTIAL= credentials('riskmaster-jenkins-ci')
	        BUILD_IMAGE='mcr.microsoft.com/dotnet/core/sdk:3.1-buster'  
	        DEPLOY_IMAGE='paragsarin/testrepo'  
            LOCAL_BUILD_IMAGE='builder'      
	    LOCAL_DEPLOY_IMAGE='deployer' 
	    	DOCKER_REGISTRY_AUTH_USERNAME="%DOCKER_REGISTRY_CREDENTIAL_USR%"
   		DOCKER_REGISTRY_AUTH_PASSWORD="%DOCKER_REGISTRY_CREDENTIAL_PSW%"
           GITHUB_DXC_CREDENTIALS=credentials('githubauth')
           GITHUB_DXC_CREDENTIALS_USERNAME="%DOCKER_REGISTRY_CREDENTIAL_USR%"
           GITHUB_DXC_CREDENTIALS_PWD="%DOCKER_REGISTRY_CREDENTIAL_PSW%"
	    CHECKOUT_BRANCH="master"
	    ECS_CLUSTER_NAME = 'EC2'
	    ECS_WEB_API_SERVICE_NAME = 'validationservice'
	    AWS_DEFAULT_REGION='us-west-2'
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
                bat "docker run --name buildcontainer_${env.BUILD_NUMBER} -e rusername=\"${env.GITHUB_DXC_CREDENTIALS_USERNAME}\" -e rpassword=\"${env.GITHUB_DXC_CREDENTIALS_PWD}\" -e AUTH_USERNAME=\"${env.DOCKER_REGISTRY_AUTH_USERNAME}\" -e AUTH_PASSWORD=\"${env.DOCKER_REGISTRY_AUTH_PASSWORD}\" -e BUILD_NUMBER=${env.BUILD_NUMBER} -w /src -d -it ${env.LOCAL_BUILD_IMAGE}" 
          		bat "docker exec buildcontainer_${env.BUILD_NUMBER} git clone --single-branch -b ${env.CHECKOUT_BRANCH} --depth=1 https://github.com/paragsarin/HALValidator.git"
				   bat "docker exec buildcontainer_${env.BUILD_NUMBER} bash -c \"cd HALValidator/Validations\""
				   bat "docker exec buildcontainer_${env.BUILD_NUMBER} dotnet restore HALValidator/Validations/Validations.csproj"
				  // bat "docker exec buildcontainer_${env.BUILD_NUMBER}  dotnet build HALValidator/Validations/Validations.csproj -c Release -o /app/build"
				    bat "docker exec buildcontainer_${env.BUILD_NUMBER}  dotnet publish HALValidator/Validations/Validations.csproj -c Release -o /app/publish"
				    bat "docker exec buildcontainer_${env.BUILD_NUMBER} bash -c \"cd /app/publish\""
				    bat "docker cp buildcontainer_${env.BUILD_NUMBER}:app/publish ."
				     bat "docker rm -f buildcontainer_${env.BUILD_NUMBER}" 
				   
				   
			   }
		     }			
		}
	       stage ("Push base images") {	
		  steps {
			   script {
			   env.WORKDIR = "${env.WORKSPACE}"
			     echo "${env.WORKDIR}"
			    echo "${env.DOCKER_REGISTRY_CREDENTIAL}"
			    echo "${env.DOCKER_REGISTRY_CREDENTIAL_PSW}"
			  //bat "docker system prune -f -a"
				 //   bat "rmdir /s /q \"./publish/Properties\""
			  bat "docker login --username \"${env.DOCKER_REGISTRY_AUTH_USERNAME}\" --password \"${env.DOCKER_REGISTRY_AUTH_PASSWORD}\"" 
			//  bat "docker pull ${env.DEPLOY_IMAGE}"
				   bat "docker build -t deploycontainernew_${env.BUILD_NUMBER} ."
               // bat "docker tag ${env.DEPLOY_IMAGE} ${env.LOCAL_DEPLOY_IMAGE}"
                //bat "docker run --name deploycontainer_${env.BUILD_NUMBER} -d -it ${env.LOCAL_BUILD_IMAGE}" 
          				// bat "docker cp ./publish deploycontainer_${env.BUILD_NUMBER}:/"
				    //bat "docker stop deploycontainer_${env.BUILD_NUMBER}"
					// bat "docker commit deploycontainer_${env.BUILD_NUMBER} deploycontainernew_${env.BUILD_NUMBER}"
					 bat "docker tag deploycontainernew_${env.BUILD_NUMBER} ${env.DEPLOY_IMAGE}"
					 bat "docker push ${env.DEPLOY_IMAGE}"
				 
				   
			   }
		     }			
		}
	    
	    stage('deploy ecs container service') {
					steps {
						echo 'deploy ecs container service'
						withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'awscrdentials', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
							bat "\"C:\\Program Files\\Amazon\\AWSCLIV2\\aws.exe\" ecs update-service --service ${env.ECS_WEB_API_SERVICE_NAME}  --cluster ${env.ECS_CLUSTER_NAME} --region ${env.AWS_DEFAULT_REGION} --force-new-deployment"
						}
						sleep(time:10,unit:"SECONDS")
						
						//withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: '07d39f8a-1812-429b-ac59-ff8a1f6a4314', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
						  //  bat "\"C:\\Program Files\\Amazon\\AWSCLI\\bin\\aws.exe\" ecs update-service --service ${env.ECS_WEB_API_SERVICE_NAME}  --cluster ${env.ECS_CLUSTER_NAME} --region ${env.AWS_DEFAULT_REGION} --desired-count ${env.ECS_DESIRED_TASK_COUNT}"
						//}
						
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
