﻿' ################################################################################
' #                             EMBER MEDIA MANAGER                              #
' ################################################################################
' ################################################################################
' # This file is part of Ember Media Manager.                                    #
' #                                                                              #
' # Ember Media Manager is free software: you can redistribute it and/or modify  #
' # it under the terms of the GNU General Public License as published by         #
' # the Free Software Foundation, either version 3 of the License, or            #
' # (at your option) any later version.                                          #
' #                                                                              #
' # Ember Media Manager is distributed in the hope that it will be useful,       #
' # but WITHOUT ANY WARRANTY; without even the implied warranty of               #
' # MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the                #
' # GNU General Public License for more details.                                 #
' #                                                                              #
' # You should have received a copy of the GNU General Public License            #
' # along with Ember Media Manager.  If not, see <http://www.gnu.org/licenses/>. #
' ################################################################################

Imports EmberAPI
Imports NLog

Public Class Scraper

#Region "Fields"

    Shared _Logger As Logger = LogManager.GetCurrentClassLogger()

    Private _Client As TMDbLib.Client.TMDbClient  'preferred language 
    Private _ClientEN As TMDbLib.Client.TMDbClient 'english language
    Private _AddonSettings As TMDB_Trailer.AddonSettings

#End Region 'Fields

#Region "Properties"

    Public Property DefaultLanguage As String
        Get
            Return _Client.DefaultLanguage
        End Get
        Set(value As String)
            _Client.DefaultLanguage = value
        End Set
    End Property

#End Region 'Properties

#Region "Methods"

    Public Async Function CreateAPI(ByVal addonSettings As TMDB_Trailer.AddonSettings) As Task
        Try
            _AddonSettings = addonSettings

            _Client = New TMDbLib.Client.TMDbClient(_AddonSettings.APIKey)
            Await _Client.GetConfigAsync()
            _Client.MaxRetryCount = 2
            _Logger.Trace("[TMDB_Trailer] [CreateAPI] Client created")

            If _AddonSettings.FallBackEng Then
                _ClientEN = New TMDbLib.Client.TMDbClient(_AddonSettings.APIKey)
                Await _ClientEN.GetConfigAsync()
                _ClientEN.DefaultLanguage = "en-US"
                _ClientEN.MaxRetryCount = 2
                _Logger.Trace("[TMDB_Trailer] [CreateAPI] Client-EN created")
            Else
                _ClientEN = _Client
                _Logger.Trace("[TMDB_Trailer] [CreateAPI] Client-EN = Client")
            End If
        Catch ex As Exception
            _Logger.Error(String.Format("[TMDB_Trailer] [CreateAPI] [Error] {0}", ex.Message))
        End Try
    End Function

    Public Function GetTrailers(ByVal TMDbID As String) As List(Of MediaContainers.Trailer)
        Dim alTrailers As New List(Of MediaContainers.Trailer)
        Dim trailers As TMDbLib.Objects.General.ResultContainer(Of TMDbLib.Objects.General.Video)

        If String.IsNullOrEmpty(TMDbID) OrElse Not Integer.TryParse(TMDbID, 0) Then Return alTrailers

        Dim APIResult As Task(Of TMDbLib.Objects.Movies.Movie)
        APIResult = Task.Run(Function() _Client.GetMovieAsync(CInt(TMDbID), TMDbLib.Objects.Movies.MovieMethods.Videos))

        trailers = APIResult.Result.Videos
        If trailers Is Nothing OrElse trailers.Results Is Nothing OrElse trailers.Results.Count = 0 AndAlso _AddonSettings.FallBackEng Then
            APIResult = Task.Run(Function() _ClientEN.GetMovieAsync(CInt(TMDbID), TMDbLib.Objects.Movies.MovieMethods.Videos))
            trailers = APIResult.Result.Videos
            If trailers Is Nothing OrElse trailers.Results Is Nothing OrElse trailers.Results.Count = 0 Then
                Return alTrailers
            End If
        End If
        If trailers IsNot Nothing AndAlso trailers.Results IsNot Nothing Then
            For Each Video As TMDbLib.Objects.General.Video In trailers.Results.Where(Function(f) f.Site = "YouTube")
                Dim tLink As String = String.Format("http://www.youtube.com/watch?v={0}", Video.Key)
                If YouTube.Scraper.IsAvailable(tLink) Then
                    Dim tName As String = YouTube.Scraper.GetVideoTitle(tLink)
                    alTrailers.Add(New MediaContainers.Trailer With {
                                           .LongLang = If(String.IsNullOrEmpty(Video.Iso_639_1), String.Empty, Localization.ISOGetLangByCode2(Video.Iso_639_1)),
                                           .Quality = GetVideoQuality(Video.Size),
                                           .Scraper = "TMDB",
                                           .ShortLang = If(String.IsNullOrEmpty(Video.Iso_639_1), String.Empty, Video.Iso_639_1),
                                           .Source = Video.Site,
                                           .Title = tName,
                                           .Type = GetVideoType(Video.Type),
                                           .URLWebsite = tLink})
                End If
            Next
        End If

        Return alTrailers
    End Function

    Private Function GetVideoQuality(ByRef Size As Integer) As Enums.TrailerVideoQuality
        Select Case Size
            Case 1080
                Return Enums.TrailerVideoQuality.HD1080p
            Case 720
                Return Enums.TrailerVideoQuality.HD720p
            Case 480
                Return Enums.TrailerVideoQuality.HQ480p
            Case Else
                Return Enums.TrailerVideoQuality.Any
        End Select
    End Function

    Private Function GetVideoType(ByRef Type As String) As Enums.TrailerType
        Select Case Type.ToLower
            Case "clip"
                Return Enums.TrailerType.Clip
            Case "featurette"
                Return Enums.TrailerType.Featurette
            Case "teaser"
                Return Enums.TrailerType.Teaser
            Case "trailer"
                Return Enums.TrailerType.Trailer
            Case Else
                Return Enums.TrailerType.Any
        End Select
    End Function

#End Region 'Methods

End Class