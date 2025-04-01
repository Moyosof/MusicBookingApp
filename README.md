# Music Booking API

## Overview

The **Music Booking API** is a RESTful service designed to manage music event bookings, offering features like artist and venue management, event scheduling, user bookings, and reservations. This API aims to streamline interactions between event organizers, artists, and fans, ensuring a smooth and organized experience.

## Features

- **Artist and Venue Management**: Manage artist profiles and venues.
- **Event Scheduling**: Create and manage events, set event dates and times, and specify venue details.
- **User Bookings and Reservations**: Allow users to book tickets.
- **Authentication and Authorization**: Secure endpoints using OAuth2 for user authentication and authorization.

## Tech Stack

- **Backend**: .NET (C#)
- **Database**: PostgreSQL
- **Authentication**: OAuth2
- **Containerization**: Docker

## API Endpoints

Here are some key endpoints available in the Music Booking API:

### **Artist Management**
- **POST** `/artists` - Create a new artist profile.
- **GET** `/artists` - Get a list of all artists.
- **GET** `/artists/{id}` - Get details of a specific artist.
- **PUT** `/artists/{id}` - Update an artist profile.

### **Event Scheduling**
- **POST** `/events` - Create a new event.
- **GET** `/events` - Get a list of all scheduled events.
- **GET** `/events/{id}` - Get details of a specific event.
- **PUT** `/events/{id}` - Update event details.

### **User Bookings**
- **POST** `/reservations` - Make a new reservation.
- **GET** `/reservations` - Get a list of all reservations.
- **GET** `/reservations/{id}` - Get details of a specific reservation.

## Authentication

This API uses OAuth2 for authentication and authorization. You will need to authenticate with a valid OAuth2 token to access most endpoints.

### OAuth2 Flow

1. **Obtain an OAuth2 token** by logging in or using a refresh token.
2. Include the token in the `Authorization` header as a Bearer token for authenticated API requests.

Example request:

```bash
GET /events
Authorization: Bearer {access_token}

## Postman Collection
You can import the Postman collection to test the API. Here’s how:

Download the Postman collection and environment file.
[Uploading Music Booking API.postman_collection.json…]()

Open Postman, go to File > Import, and select the downloaded collection and environment files.
[Uploading MusicBookingAPI.postman_environment.json…]()


Set up your environment with the appropriate API base URL and OAuth2 credentials.
