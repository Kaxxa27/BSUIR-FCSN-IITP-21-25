#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <pthread.h>

#define DEFAULT_PORT 12345
#define BUFFER_SIZE 2056
#define MAX_CLIENTS 10

struct ClientInfo {
    int socket;
    char clientId[20];
};

struct ClientInfo clients[MAX_CLIENTS];
int num_clients = 0;

char* generate_client_id() {
    char* client_id = malloc(20 * sizeof(char));
    if (client_id == NULL) {
        perror("Memory allocation failed");
        exit(EXIT_FAILURE);
    }

    snprintf(client_id, 20, "%d", rand());
    return client_id;
}

void* handle_client(void* client_info_ptr) {
    struct ClientInfo* client_info = (struct ClientInfo*)client_info_ptr;
    int client_socket = client_info->socket;
    char buffer[BUFFER_SIZE];

    while (1) {
        int bytes_received = recv(client_socket, buffer, sizeof(buffer) - 1, 0);
        if (bytes_received == -1 || bytes_received == 0) {
            printf("Client %s disconnected\n", client_info->clientId);
            close(client_socket);
            return NULL;
        }

        buffer[bytes_received] = '\0';
        printf("Received from client %s: %s\n", client_info->clientId, buffer);

        for (int i = 0; i < num_clients; ++i) {
            if (clients[i].socket != client_socket) {
                char message_with_id[BUFFER_SIZE];
                snprintf(message_with_id, sizeof(message_with_id), "%s: %s", client_info->clientId, buffer);
                send(clients[i].socket, message_with_id, strlen(message_with_id), 0);
            }
        }
    }
}

int main() {
    struct sockaddr_in server_addr, client_addr;
    socklen_t client_addr_len = sizeof(client_addr);
    pthread_t client_threads[MAX_CLIENTS];

    int server_socket = socket(AF_INET, SOCK_STREAM, 0);
    if (server_socket == -1) {
        perror("Failed to create server socket");
        exit(EXIT_FAILURE);
    }

    server_addr.sin_family = AF_INET;
    server_addr.sin_addr.s_addr = INADDR_ANY;
    server_addr.sin_port = htons(DEFAULT_PORT);

    if (bind(server_socket, (struct sockaddr*)&server_addr, sizeof(server_addr)) == -1) {
        perror("Failed to bind server socket");
        close(server_socket);
        exit(EXIT_FAILURE);
    }

    if (listen(server_socket, SOMAXCONN) == -1) {
        perror("Error in listen function");
        close(server_socket);
        exit(EXIT_FAILURE);
    }

    printf("Server is listening on port %d...\n", DEFAULT_PORT);

    while (1) {
        int client_socket = accept(server_socket, (struct sockaddr*)&client_addr, &client_addr_len);
        if (client_socket == -1) {
            perror("Failed to accept client connection");
            continue;
        }

        if (num_clients >= MAX_CLIENTS) {
            printf("Maximum number of clients reached. Closing new connection.\n");
            close(client_socket);
            continue;
        }

        struct ClientInfo new_client;
        new_client.socket = client_socket;
        strncpy(new_client.clientId, generate_client_id(), sizeof(new_client.clientId));

        printf("Client connected: %s\n", new_client.clientId);

        clients[num_clients++] = new_client;

        pthread_create(&client_threads[num_clients - 1], NULL, handle_client, (void*)&clients[num_clients - 1]);
    }

}
